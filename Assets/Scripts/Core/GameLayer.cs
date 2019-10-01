using UnityEngine;

public class GameLayer : MonoBehaviour
{
    public static GameLayer I;
    public bool EmulateServer;

    public SceneController SceneController { get; private set; }
    public ServerEmulator ServerEmulator { get; private set; }
    public NetworkController Net { get; private set; }
    public GameBalance GameBalance { get; private set; }

    public void Start()
    {
        DontDestroyOnLoad(this);

        I = this;

        GameBalance = new GameBalance();
        SceneController = new SceneController();
        ServerEmulator = new ServerEmulator();
        Net = new NetworkController();
        Net.Auth();

        if (EmulateServer)
        {
            ServerEmulator.Login();
        }
    }

    private void Update()
    {
    }

    private void OnApplicationQuit()
    {
        Net.Disconnect();
    }
}
