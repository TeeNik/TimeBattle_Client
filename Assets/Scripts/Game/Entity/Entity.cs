using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{

    public int Id { get; private set; }
    protected List<ComponentBase> Components;

    public void Init(int id, IEnumerable<ComponentBase> initial = null)
    {
        Id = id;
        Components = new List<ComponentBase>();

        foreach (var component in GetComponents<ComponentBase>())
        {
            if (initial != null)
            {
                var data = initial.First(c => c.GetType() == component.GetType());
                if (data != null)
                {
                    component.UpdateComponent(data);
                }
            }

            Components.Add(component);
            Game.I.SystemController.Systems[component.GetType()].AddComponent(Id, component);
        }
    }

    public T GetEcsComponent<T>()
    {
        var component = (T)Components.Find(c => c.GetType() == typeof(T));
        return component;
    }

    public ComponentBase GetEcsComponent(Type type)
    {
        return Components.Find(c => c.GetType() == type);
    }
}
