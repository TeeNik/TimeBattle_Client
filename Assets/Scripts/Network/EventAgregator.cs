using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

    public void ProcessEvent(string header, BaseEventClass evt)
    {
        if (_events.ContainsKey(header))
        {
            var 
        }
    }

    public BaseEventClass GetEvent(string header)
    {
        return _events[header];
    }

}
