using System;
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
    public MapController MapController;
    public UserInputController UserInputController;
    public GameUI GameUI;

    public PlayerType PlayerType = PlayerType.Player1;
    public GameState GameState = GameState.UserInput;

    public FlagController flagController;

    private List<ActionPhase> _turnData;

    private int _currentPhase;

    private int _phaseLength;
    private int _updatesCount;

    private readonly EventListener _eventListener = new EventListener();

    private void Start()
    {
        I = this;

        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(.5f);

        Messages = new GameEventDispatcher();
        SystemController = new SystemController();
        EntityManager = new EntityController();
        MapController.Init();
        UserInputController.Init();
        GameUI.Init();

        flagController = new FlagController();

        //TODO Remove later
        GameLayer.I.ServerEmulator.Start();
    }

    public void OnTurnData(List<ActionPhase> data)
    {
        _turnData = data.ToList();
        _currentPhase = 0;
        ProducePhase();
    }
    
    //TODO make this function cleaner
    public void ProducePhase()
    {

        if (_currentPhase > _turnData.Max(t => t.phases.Count))
        {
            _currentPhase = 0;
            SystemController.OnUpdateEnd();
            CheckEndGame();
            Messages.SendEvent(EventStrings.OnNextTurn);
            return;
        }

        /*if (_currentPhase != 0)
        {
            SystemController.OnUpdateEnd();
        }*/

        foreach (var dto in _turnData)
        {
            if (dto.phases.Count > _currentPhase)
            {
                SystemController.ProcessData(dto.entityId, dto.phases[_currentPhase].ToComponentBase());
                _phaseLength = SystemController.GetPhaseLength();
                _updatesCount = 0;
            }
        }

        ++_currentPhase;
        IterateOverPhase();
    }

    private IEnumerator WaitForNextIteration()
    {
        do
        {
            yield return new WaitForSeconds(.01f);
        } while (SystemController.IsProcessing());

        IterateOverPhase();
    }

    public void IterateOverPhase()
    {
        if (_updatesCount < _phaseLength)
        {
            ++_updatesCount;
            SystemController.UpdateSystems();
            StartCoroutine(WaitForNextIteration());
        }
        else
        {
            ProducePhase();
        }
    }

    private void CheckEndGame()
    {
        var player1 = SystemController.GetSystem<OperativeInfoSystem>().GetEntitiesByOwner(PlayerType.Player1).Count;
        var player2 = SystemController.GetSystem<OperativeInfoSystem>().GetEntitiesByOwner(PlayerType.Player2).Count;
        if (player1 == 0 && player2 == 0)
        {
            Debug.Log("Draw");
        }
        else if (player1 == 0)
        {
            Debug.Log("player2 wins!");
        }
        else if (player2 == 0)
        {
            Debug.Log("player1 wins!");
        }
    }

    void OnDestroy()
    {
        SystemController.Dispose();
        _eventListener.Clear();
        Messages.Dispose();
    }
}
