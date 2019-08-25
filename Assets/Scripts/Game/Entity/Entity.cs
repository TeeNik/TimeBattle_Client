using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{

    public int Id { get; private set; }
    protected List<ComponentBase> Components;

    public void Init(int id)
    {
        Id = id;
        Components = new List<ComponentBase>();
    }

    public void AddComponent(ComponentBase component)
    {
        Components.Add(component);
    }

    public void RemoveComponent(ComponentBase component)
    {
        Components.Remove(component);
    }


}
