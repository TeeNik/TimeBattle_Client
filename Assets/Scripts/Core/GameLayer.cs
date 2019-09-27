using UnityEngine;

public class GameLayer : MonoBehaviour
{
    public static GameLayer I;
    public bool EmulateServer;

    public SceneController SceneController { get; private set; }
    public ServerEmulator ServerEmulator { get; private set; }
    public NetworkController Net { get; private set; }

    public struct Data
    {
        public int Move;
    }

    public void Start()
    {
        DontDestroyOnLoad(this);

        I = this;

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
