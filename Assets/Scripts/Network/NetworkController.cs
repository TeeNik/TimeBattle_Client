using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class NetworkController
{
    private readonly string Address = "ws://116.203.77.112:8080/multiplayer/rand";

    private WebSocket _ws;

    public void Auth()
    {
        _ws = new WebSocket(Address);
        _ws.OnMessage += OnMessage;
        _ws.OnOpen += OnConnectionOpen;
        _ws.OnClose += OnConnectionClose;
        _ws.OnError += OnConnectionError;

        _ws.Connect();
    }

    public void Disconnect()
    {
        _ws.Close();
    }

    private void OnConnectionError(object sender, ErrorEventArgs args)
    {
        Debug.Log(args);
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
        Debug.Log(args.Data);
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
