using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using WebSocketSharp;

public class NetworkController : MonoBehaviour
{
    //private readonly string Address = "ws://116.203.77.112:8080/multiplayer/rand";
    private readonly string Address = "ws://localhost:8080/multiplayer/rand";

    private WebSocket _ws;
    private EventAgregator _eventAgregator;
    private Queue<string> _eventQueue;

    public void Auth()
    {
        _eventAgregator = new EventAgregator();
        _eventQueue = new Queue<string>();

        _ws = new WebSocket(Address);
        _ws.OnMessage += OnMessage;
        _ws.OnOpen += OnConnectionOpen;
        _ws.OnClose += OnConnectionClose;
        _ws.OnError += OnConnectionError;

        //if (!GameLayer.I.EmulateServer)uni
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
        /*var login = new LoginMsg{ header = "login", IMEI = "123"};
        var join = new JoinMsg { header = "joingame"};

        Debug.Log(JsonUtility.ToJson(login));
        _ws.Send(JsonUtility.ToJson(login));
        _ws.Send(JsonUtility.ToJson(join));*/

        Send(_eventAgregator.GetEvent<LoginEvent>().Send());
    }

    private void OnMessage(object sender, MessageEventArgs args)
    {
        _eventQueue.Enqueue(args.Data);
        //ProcessEvent(JObject.Parse(args.Data));
    }

    void Update()
    {
        if (_eventQueue.Any())
        {
            ProcessEvent(JObject.Parse(_eventQueue.Dequeue()));
        }
    }

    public void Send(string json)
    {
        _ws.Send(json);
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
