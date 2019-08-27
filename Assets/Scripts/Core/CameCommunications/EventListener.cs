using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListener
{

    private readonly List<KeyValuePair<Type, Delegate>> _subscriptions;

    public EventListener()
    {
        _subscriptions = new List<KeyValuePair<Type, Delegate>>();
    }

    public void Add<T>(Action<T> a)
    {
        _subscriptions.Add(new KeyValuePair<Type, Delegate>(typeof(T), a));
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
