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

    protected override void HandleResponseImpl(JObject json)
    {
        var data = JsonConvert.DeserializeObject<List<List<ActionPhase>>>(json["turnData"].ToString());
        var union = data[0].Union(data[1]).ToList();
        Game.I.OnTurnData(union);
    }
}
