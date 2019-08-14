using UnityEngine;

public class GameLayer : MonoBehaviour
{
    public static GameLayer I;

    public SystemController SystemController { get; private set; } 
    public EntityManager EntityManager { get; private set; }

    public void Start()
    {
        I = this;

        SystemController = new SystemController();
        EntityManager = new EntityManager();
    }

    //TODO Incapsulate
    public void SendSystemUpdate()
    {
        SystemController.UpdateSystems();
    }

}
