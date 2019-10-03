using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

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
            Debug.Log(json.ToString().Replace('\n', ' '));

            var evnt = _events[cmd];

            JObject data = null;
            var param = json["params"];
            if (param != null && param.HasValues)
            {
                data = new JObject(param.ToString());
            }

            evnt.HandleResponse(data);
        }
    }

    public T GetEvent<T>() where T : BaseEventClass
    {
        return (T)_events.Values.First(c => c.GetType() == typeof(T));
    }

    public BaseEventClass GetEvent(string header)
    {
        return _events[header];
    }

}
