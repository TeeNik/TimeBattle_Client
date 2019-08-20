using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class EventAgregator
{

    private Dictionary<string, BaseEventClass> _events;

    public void InitEvents()
    {
        var t = typeof(BaseEventClass);
        var types = Assembly.GetAssembly(t).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && t.IsAssignableFrom(myType));
        foreach (Type type in types)
        {
            var evebtBase = (BaseEventClass)Activator.CreateInstance(type);
            _events.Add(evebtBase.Type, command);
        }
    }

}
