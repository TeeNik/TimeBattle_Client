using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Entity : MonoBehaviour
{

    public int Id { get; private set; }
    protected List<ComponentBase> Components;
    public bool IsDestroyed { get; private set; }

    public void Init(int id)
    {
        Id = id;
        Components = new List<ComponentBase>();
    }

    public void AddComponent(ComponentBase component)
    {
        Components.Add(component);
        var systems = Game.I.SystemController.Systems;
        var type = component.GetType();
        Assert.IsTrue(systems.ContainsKey(type), $"systems has no {type}");
        systems[type].AddComponent(this, component);
    }

    public void RemoveComponent(ComponentBase component)
    {
        Components.Remove(component);
        Game.I.SystemController.Systems[component.GetType()].RemoveComponent(Id);
    }

    private void ClearComponents()
    {
        var toDelete = Components.ToArray();
        foreach(var comp in toDelete)
        {
            RemoveComponent(comp);
        }
    }

    public void Destroy()
    {
        IsDestroyed = true;
        ClearComponents();
    }

    public T GetEcsComponent<T>() where T : ComponentBase
    {
        var component = (T)Components.Find(c => c.GetType() == typeof(T));
        return component;
    }

    public ComponentBase GetEcsComponent(Type type)
    {
        return Components.Find(c => c.GetType() == type);
    }
}