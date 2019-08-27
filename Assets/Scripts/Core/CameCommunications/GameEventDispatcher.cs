using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventDispatcher : IDisposable
{

    private Dictionary<Type, List<Delegate>> _subscribers;

    public GameEventDispatcher()
    {
        _subscribers = new Dictionary<Type, List<Delegate>>();
    }

    public Action<T> Subscribe<T>(Action<T> handler)
    {
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
        return handler;
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

    public void Unsubscribe(Type type, Delegate handler)
    {
        if (_subscribers.ContainsKey(type))
        {
            _subscribers[type].Remove(handler);
        }
    }

    public void Dispose()
    {
        foreach (var pair in _subscribers)
        {
            pair.Value.Clear();
        }
        _subscribers.Clear();
    }
}
