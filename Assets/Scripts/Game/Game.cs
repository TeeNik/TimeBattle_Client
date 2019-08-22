using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game I { get; private set; }

    public SystemController SystemController { get; private set; }
    public EntityManager EntityManager { get; private set; }
    public MapController MapController;
    public InputDataController InputController;

    private List<ActionDto> _turnData;
    private int _currentPhase;

    private void Start()
    {
        I = this;

        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(2);

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
        EntityManager.CreatePlayer();
        SendSystemUpdate();
    }

    public void OnTurnData(List<ActionDto> data)
    {
        _turnData = data.ToList();
        _currentPhase = 0;
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
        SystemController.UpdateSystems();
        ++_currentPhase;
    }
}
