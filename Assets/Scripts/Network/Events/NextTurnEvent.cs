using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class NextTurnEvent : BaseEventClass
{

    public NextTurnEvent() : base("nextTurn")
    {
    }

    public class NextTurnData
    {
        public List<string> turnData;
    }

    protected override void HandleResponseImpl(JObject json)
    {
        var data = JsonConvert.DeserializeObject<NextTurnData>(json.ToString());
        var list = new List<ActionPhase>();
        list.AddRange(JsonConvert.DeserializeObject<List<ActionPhase>>(data.turnData[0]));
        list.AddRange(JsonConvert.DeserializeObject<List<ActionPhase>>(data.turnData[1]));
        Game.I.UserInputController.ProcessTurn(list);
    }
}
