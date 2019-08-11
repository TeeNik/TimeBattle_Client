using UnityEngine;

public class GameLayer : MonoBehaviour
{

    public SystemController SystemController { get; private set; } 
    public EntityManager EntityManager { get; private set; }

    public void Start()
    {
        SystemController = new SystemController();
        EntityManager = new EntityManager();
    }

}
