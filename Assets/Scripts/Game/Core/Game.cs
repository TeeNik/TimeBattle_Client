using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game I { get; private set; }

    public SystemController SystemController { get; private set; }
    public EntityController EntityManager { get; private set; }
    public GameEventDispatcher Messages { get; private set; }
    public EntitySpawner EntitySpawner { get; private set; }
    
    public MapController MapController;
    public UserInputController UserInputController;
    public GameUI GameUI;

    public PlayerType PlayerType = PlayerType.Player1;
    public GameState GameState = GameState.UserInput;

    public FlagController flagController;
    private List<ActionPhase> _turnData;
    private int _currentPhase;
    private readonly EventListener _eventListener = new EventListener();

    private void Start()
    {
        I = this;

        Messages = new GameEventDispatcher();
        SystemController = new SystemController();
        EntityManager = new EntityController();
        EntitySpawner = new EntitySpawner();
        MapController.Init();
        UserInputController.Init();
        GameUI.Init();

        flagController = new FlagController();

        EntitySpawner.StartGame();
    }

    public void OnTurnData(List<ActionPhase> data)
    {
        _turnData = data.ToList();
        _currentPhase = 0;
        ProducePhase();
    }
    
    public void ProducePhase()
    {
        //TODO 10!!
        if(_currentPhase == 10)
        {
            _currentPhase = 0;
            SystemController.OnUpdateEnd();
            CheckEndGame();
            Messages.SendEvent(EventStrings.OnNextTurn);
            return;
        }

        foreach(var actionPhase in _turnData)
        {
            foreach (var dto in actionPhase.dtos)
            {
                if (dto.StartTick == _currentPhase)
                {
                    SystemController.ProcessData(actionPhase.entityId, dto.ToComponentBase());
                }
            }
        }
        
        ++_currentPhase;
        StartCoroutine(WaitForNextIteration());
    }

    private IEnumerator WaitForNextIteration()
    {
        SystemController.UpdateSystems();
        do
        {
            yield return new WaitForSeconds(.01f);
        } while (SystemController.IsProcessing());

        ProducePhase();
    }

    private void CheckEndGame()
    {
        var player1 = SystemController.GetSystem<OperativeInfoSystem>().GetEntitiesByOwner(PlayerType.Player1).Count;
        var player2 = SystemController.GetSystem<OperativeInfoSystem>().GetEntitiesByOwner(PlayerType.Player2).Count;
        if (player1 == 0 || player2 == 0)
        {
            PlayerType? winner = null;
            if (player1 == 0 && player2 == 0)
            {
                Debug.Log("Draw");
            }
            else if (player1 == 0)
            {
                winner = PlayerType.Player2;
                Debug.Log("player2 wins!");
            }
            else if (player2 == 0)
            {
                winner = PlayerType.Player1;
                Debug.Log("player1 wins!");
            }
            Messages.SendEvent(new PlayerWinMsg(winner));
        }
    }

    void OnDestroy()
    {
        SystemController.Dispose();
        _eventListener.Clear();
        Messages.Dispose();
    }
}
