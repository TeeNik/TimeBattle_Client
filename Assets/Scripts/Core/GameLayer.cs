using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WebSocketSharp;

public class GameLayer : MonoBehaviour
{
    public static GameLayer I;

    private WebSocket ws;

    public struct Data
    {
        public int Move;
    }

    public void Start()
    {
        I = this;

        ws = new WebSocket("ws://116.203.77.112:8080/multiplayer/rand");
        ws.OnMessage += (sender, e) =>
        Debug.Log("Petros says: " + e.Data);

        ws.OnOpen += OnSocketOpen;
        ws.OnClose += OnClose;
        ws.OnError += Ws_OnError;

        ws.Connect();



        List<Data> list = new List<Data>();
    }

    private void Update()
    {
    }

    private void OnClose(object sender, CloseEventArgs e)
    {
        Debug.Log(e);
    }

    private void Ws_OnError(object sender, ErrorEventArgs e)
    {
        Debug.Log(sender);
    }

    private void OnSocketOpen(object sender, System.EventArgs e)
    {
        Debug.Log(e);
        //ws.Send("hello petros");
    }
}
