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
    public PlayerType PlayerType = PlayerType.Player1;

    private List<ActionDto> _turnData;

    private int _currentPhase;
    private int _currentPhaseAction;

    private void Start()
    {
        I = this;

        StartCoroutine(DelayedStart());
    }


    private EventListener _eventListener;

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(1);

        Messages = new GameEventDispatcher();
        SystemController = new SystemController();
        EntityManager = new EntityManager();
        MapController.Init();

        InitialEvent();
    }

    //TODO Incapsulate
    public void SendSystemUpdate()
    {
        SystemController.UpdateSystems();
    }

    //TODO Replace by server event
    private void InitialEvent()
    {
        var spawn1 = new SpawnEntityDto();
        spawn1.operativeInfo = new OperativeInfoCmponent(PlayerType.Player1, OperativeType.Soldier);
        spawn1.spawnPosition = new MovementComponent(new Point(8, 8));

        var spawn2 = new SpawnEntityDto();
        spawn2.operativeInfo = new OperativeInfoCmponent(PlayerType.Player2, OperativeType.Soldier);
        spawn2.spawnPosition = new MovementComponent(new Point(9, 4));


        EntityManager.CreatePlayer(spawn1);
        EntityManager.CreatePlayer(spawn2);



        SendSystemUpdate();
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
        else
        {
        }
    }

    void OnDestroy()
    {
        Messages.Dispose();
    }
}
