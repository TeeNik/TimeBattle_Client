using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor;
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
            //["params"] = JsonConvert.SerializeObject(param)
        };
        if(param != null)
        {
            json.Merge(param);
        }
        return json;
    }

    public void PlayGame()
    {
        JObject param = new JObject();
        param["playerType"] = (int)PlayerType.Player1;
        GameLayer.I.Net.ProcessEvent(CreateEventMessage("startGame", param));
    }

}
