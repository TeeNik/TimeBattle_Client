using System;
using System.Collections.Generic;
using SimpleJSON;

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

    public void ProcessEvent(JSONObject json)
    {
        var cmd = json["cmd"].Value;
        if (_events.ContainsKey(cmd))
        {
            var evnt = _events[cmd];
            var data = json["params"].AsObject;

            evnt.HandleResponse(data);
        }
    }

    public BaseEventClass GetEvent(string header)
    {
        return _events[header];
    }

}
