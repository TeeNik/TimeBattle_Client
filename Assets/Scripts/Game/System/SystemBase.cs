using System.Collections.Generic;

public abstract class SystemBase<T> where T : ComponentBase
{
    protected Dictionary<int, T> Components;
    protected List<int> ToDelete;

    protected SystemBase()
    {
        ToDelete = new List<int>();
        Components = new Dictionary<int, T>();
    }

    public void Update()
    {
        UpdateImpl();

        foreach (var key in ToDelete)
        {
            Components.Remove(key);
        }
    }

    public abstract void UpdateImpl();

    public void AddComponent(int entityId, T component)
    {
        Components.Add(entityId, component);
    }

    public void RemoveComponent(int entityId)
    {
        Components.Remove(entityId);
    }
}
