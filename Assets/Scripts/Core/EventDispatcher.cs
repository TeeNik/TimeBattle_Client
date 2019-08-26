using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDispatcher
{

    private Dictionary<Type, List<Delegate>> _subscribers;

    public EventDispatcher()
    {
        _subscribers = new Dictionary<Type, List<Delegate>>();
    }

    public void Subscribe<T>(Action<T> handler)
    {
        Action<int> o = (__) => { };
        var type = typeof(T);
        if (_subscribers.ContainsKey(type))
        {
            _subscribers[type].Add(handler);
        }
        else
        {
            var handlers = new List<Delegate>();
            handlers.Add(handler);
            _subscribers.Add(type, handlers);
        }
    }

    public void SendEvent<T>(T obj)
    {
        var type = typeof(T);
        if (_subscribers.ContainsKey(type))
        {
            foreach(var handler in _subscribers[type])
            {
                handler.DynamicInvoke(obj);
            }
        }
    }



}
