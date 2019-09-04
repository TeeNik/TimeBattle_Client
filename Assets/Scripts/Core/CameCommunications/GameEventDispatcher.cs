using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventDispatcher : IDisposable
{

    private readonly Dictionary<string, List<Delegate>> _subscribers;

    public GameEventDispatcher()
    {
        _subscribers = new Dictionary<string, List<Delegate>>();
    }

    public KeyValuePair<string, Delegate> Subscribe(string key, Action handler)
    {
        if (_subscribers.ContainsKey(key))
        {
            _subscribers[key].Add(handler);
        }
        else
        {
            var handlers = new List<Delegate>();
            handlers.Add(handler);
            _subscribers.Add(key, handlers);
        }
        return new KeyValuePair<string, Delegate>(key, handler);
    }

    public KeyValuePair<string, Delegate> Subscribe<T>(Action<T> handler)
    {
        var type = typeof(T).ToString();
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
        return new KeyValuePair<string, Delegate>(type, handler);
    }

    public void SendEvent<T>(T obj)
    {
        var type = typeof(T).ToString();
        if (_subscribers.ContainsKey(type))
        {
            foreach(var handler in _subscribers[type])
            {
                handler.DynamicInvoke(obj);
            }
        }
    }

    public void SendEvent(string key)
    {
        if (_subscribers.ContainsKey(key))
        {
            foreach (var handler in _subscribers[key])
            {
                handler.DynamicInvoke();
            }
        }
    }

    public void Unsubscribe(Type type, Delegate handler)
    {
        var key = type.ToString();
        if (_subscribers.ContainsKey(key))
        {
            _subscribers[key].Remove(handler);
        }
    }

    public void Unsubscribe(string type, Delegate handler)
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
