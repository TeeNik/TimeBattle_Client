using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WebSocketSharp;

public class GameLayer : MonoBehaviour
{
    public static GameLayer I;

    public NetworkController Net { get; private set; }

    public struct Data
    {
        public int Move;
    }

    public void Start()
    {
        I = this;

        Net = new NetworkController();
        Net.Auth();
    }

    private void Update()
    {
    }

    private void OnApplicationQuit()
    {
        Net.Disconnect();
    }
}
