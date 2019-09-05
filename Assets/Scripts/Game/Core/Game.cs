using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game I { get; private set; }

    public SystemController SystemController { get; private set; }
    public EntityManager EntityManager { get; private set; }
    public GameEventDispatcher Messages { get; private set; }
    public MapController MapController;
    public InputDataController InputController;
    public GameUI GameUI;

    public PlayerType PlayerType = PlayerType.Player1;
    public GameState GameState = GameState.UserInput;

    private List<ActionDto> _turnData;

    private int _currentPhase;
    private int _currentPhaseAction;

    private readonly EventListener _eventListener = new EventListener();
    private readonly ServerEmulator _serverEmulator = new ServerEmulator();

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
        EntityManager = new EntityManager();
        MapController.Init();
        InputController.Init();
        GameUI.Init();

        //TODO make optional
        _serverEmulator.Start();
    }

    public void OnTurnData(List<ActionDto> data)
    {
        _turnData = data.ToList();
        _currentPhase = 0;
        ProducePhase();
    }

    public void ProducePhase()
    {
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
            _currentPhaseAction++;
        }
    }

    void OnDestroy()
    {
        SystemController.Dispose();
        _eventListener.Clear();
        Messages.Dispose();
    }
}
