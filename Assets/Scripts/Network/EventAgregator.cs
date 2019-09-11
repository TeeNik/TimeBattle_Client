using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class EventAgregator
{

    private Dictionary<string, BaseEventClass> _events;

    public EventAgregator()
    {
        _events = new Dictionary<string, BaseEventClass>();
        var type = typeof(BaseEventClass);
        var types = Utils.GetTypesOfParent(type);
        foreach (Type t in types)
        {
            var eventBase = (BaseEventClass)Activator.CreateInstance(t);
            _events.Add(eventBase.Header, eventBase);
        }
    }

    public void ProcessEvent(JObject json)
    {
        var cmd = json["cmd"].ToString();
        if (_events.ContainsKey(cmd))
        {
            var evnt = _events[cmd];

            var param = json["params"];
            JObject data = null;
            if (param.HasValues)
            {
                data = new JObject(param.ToString());
            }
            evnt.HandleResponse(data);
        }
    }

    public BaseEventClass GetEvent(string header)
    {
        return _events[header];
    }

}
