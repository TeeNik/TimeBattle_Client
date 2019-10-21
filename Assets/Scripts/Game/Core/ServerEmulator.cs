using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ServerEmulator
{

    private int _entityCounter = 0;

    public void Login()
    {
        GameLayer.I.Net.ProcessEvent(CreateEventMessage("login", null));
    }

    private JObject CreateEventMessage(string cmd, object param)
    {
        var json = new JObject
        {
            ["cmd"] = cmd,
            ["params"] = JsonUtility.ToJson(param)
        };
        return json;
    }

    public void PlayGame()
    {
        GameLayer.I.Net.ProcessEvent(CreateEventMessage("startGame", null));
    }

}
