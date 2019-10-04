using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class NextTurnEvent : BaseEventClass
{

    public NextTurnEvent() : base("nextTurn")
    {
    }

    protected override void HandleResponseImpl(JObject json)
    {
        
    }
}
