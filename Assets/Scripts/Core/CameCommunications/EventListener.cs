using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListener
{

    private readonly List<KeyValuePair<string, Delegate>> _subscriptions;

    public EventListener()
    {
        _subscriptions = new List<KeyValuePair<string, Delegate>>();
    }

    public void Add<T>(KeyValuePair<string, Delegate> a)
    {
        _subscriptions.Add(a);
    }

    public void Add(KeyValuePair<string, Delegate> a)
    {
        _subscriptions.Add(a);
    }

    public void Clear()
    {
        foreach (var subscription in _subscriptions)
        {
            var type = subscription.Key;
            Game.I.Messages.Unsubscribe(type, subscription.Value);
        }
        _subscriptions.Clear();
    }

}
