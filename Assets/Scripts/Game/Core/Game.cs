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
    public InputDataController InputController;
    public GameUI GameUI;

    public PlayerType PlayerType = PlayerType.Player1;
    public GameState GameState = GameState.UserInput;

    private List<ActionPhase> _turnData;

    private int _currentPhase;

    private readonly EventListener _eventListener = new EventListener();

    private void Start()
    {
        I = this;

        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(1);

        Messages = new GameEventDispatcher();
        SystemController = new SystemController();
        EntityManager = new EntityController();
        MapController.Init();
        InputController.Init();
        GameUI.Init();

        //TODO Remove later
        GameLayer.I.ServerEmulator.Start();
    }

    public void OnTurnData(List<ActionPhase> data)
    {
        _turnData = data.ToList();
        _currentPhase = 0;
        ProducePhase();
    }

    public void ProducePhase()
    {
        if(_currentPhase > _turnData.Max(t => t.phases.Count))
        {
            _currentPhase = 0;
            SystemController.OnUpdateEnd();
            CheckEndGame();
            return;
        }

        foreach (var dto in _turnData)
        {
            if (dto.phases.Count > _currentPhase)
            {
                SystemController.ProcessData(dto.entityId, dto.phases[_currentPhase]);
            }
        }

        ++_currentPhase;
        IterateOverPhase();
    }

    public void IterateOverPhase()
    {
        if (SystemController.IsProcessing())
        {
            SystemController.UpdateSystems();
        }
        else
        {
            ProducePhase();
        }
    }

    private void CheckEndGame()
    {
        var player1 = SystemController.GetSystem<OperativeInfoSystem>().GetEntitiesByOwner(PlayerType.Player1);
        var player2 = SystemController.GetSystem<OperativeInfoSystem>().GetEntitiesByOwner(PlayerType.Player2);
        if (player1.Count == 0)
        {
            Debug.Log("player2 wins!");
        }
        else if (player2.Count == 0)
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
