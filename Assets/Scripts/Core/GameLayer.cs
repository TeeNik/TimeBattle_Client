using UnityEngine;

public class GameLayer : MonoBehaviour
{
    public static GameLayer I;

    public SystemController SystemController { get; private set; } 
    public EntityManager EntityManager { get; private set; }
    public MapController MapController;

    public void Start()
    {
        I = this;

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
