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
        var types = Assembly.GetAssembly(type).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && type.IsAssignableFrom(myType));
        foreach (Type t in types)
        {
            var eventBase = (BaseEventClass)Activator.CreateInstance(t);
            _events.Add(eventBase.Header, eventBase);
        }
    }

    public BaseEventClass GetEvent(string header)
    {
        return _events[header];
    }

}
