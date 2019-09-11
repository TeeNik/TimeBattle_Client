using System;
using Newtonsoft.Json.Linq;
using UnityEngine;
using WebSocketSharp;

public class NetworkController
{
    private readonly string Address = "ws://116.203.77.112:8080/multiplayer/rand";

    private WebSocket _ws;
    private EventAgregator _eventAgregator;

    public void Auth()
    {
        _eventAgregator = new EventAgregator();

        _ws = new WebSocket(Address);
        _ws.OnMessage += OnMessage;
        _ws.OnOpen += OnConnectionOpen;
        _ws.OnClose += OnConnectionClose;
        _ws.OnError += OnConnectionError;

        if (!GameLayer.I.EmulateServer)
        {
            _ws.Connect();
        }
    }

    public void Disconnect()
    {
        _ws.Close();
    }

    private void OnConnectionError(object sender, ErrorEventArgs args)
    {
        Debug.Log(args.Exception);
    }

    private void OnConnectionClose(object sender, CloseEventArgs args)
    {
        Debug.Log(args);
    }

    private void OnConnectionOpen(object sender, EventArgs args)
    {
        Debug.Log(args);
        var login = new LoginMsg{ header = "login", IMEI = "123"};
        var join = new JoinMsg { header = "joingame"};

        Debug.Log(JsonUtility.ToJson(login));
        _ws.Send(JsonUtility.ToJson(login));
        _ws.Send(JsonUtility.ToJson(join));
    }

    private void OnMessage(object sender, MessageEventArgs args)
    {
        ProcessEvent(new JObject(args.Data));
    }

    public void ProcessEvent(JObject json)
    {
        _eventAgregator.ProcessEvent(json);
    }

    public class LoginMsg
    {
        public string IMEI;
        public string header;
    }

    public class JoinMsg
    {
        public string header;
    }
}
