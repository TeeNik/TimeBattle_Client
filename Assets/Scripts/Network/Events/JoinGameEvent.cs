using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class JoinGameEvent : BaseEventClass
{
    public JoinGameEvent() : base("joinGame")
    {
    }

    protected override void HandleResponseImpl(JObject json)
    {
        
    }
}
