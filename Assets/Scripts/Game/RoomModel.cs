using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomModel : MonoBehaviour
{
    public static RoomModel I { get; private set; }
    public Game Game;

    public SystemController SystemController { get; private set; }
    public EntityManager EntityManager { get; private set; }
    public MapController MapController;
    public InputDataController InputController;

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

}
