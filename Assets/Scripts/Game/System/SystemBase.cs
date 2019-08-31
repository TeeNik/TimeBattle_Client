using System.Collections.Generic;

/*public abstract class SystemBase<T> where T : ComponentBase
{
    protected Dictionary<int, T> Components;

    protected SystemBase()
    {
        Components = new Dictionary<int, T>();
    }

    public virtual void Update()
    {
        UpdateImpl();
    }

    public virtual void OnComponentAdded(int entityId, T component)
    {

    }

    public abstract void UpdateImpl();

    public void AddComponent(int entityId, T component)
    {
        Components.Add(entityId, component);
        OnComponentAdded(entityId, component);
    }

    public void RemoveComponent(int entityId)
    {
        Components.Remove(entityId);
    }

    public void ClearSystem()
    {
        Components.Clear();
    }

    public T GetComponent(int entityId)
    {
        return Components[entityId];
    }
}*/

public interface ISystem
{
    void Update();
    void AddComponent(Entity entity, ComponentBase component);
    void RemoveComponent(int entityId);
    bool IsProcessing();
}
