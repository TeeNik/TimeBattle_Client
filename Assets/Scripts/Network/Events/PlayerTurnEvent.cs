using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class PlayerTurnEvent : BaseEventClass
{
    public PlayerTurnEvent() : base("playerTurn")
    {
    }

    public string SetData(List<ActionPhase> turnData)
    {
        Json["turnData"] = JsonConvert.SerializeObject(turnData);
        return Json.ToString();
    }

    protected override void HandleResponseImpl(JObject json)
    {
        
    }
}
