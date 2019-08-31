using System.Collections.Generic;

public class OperativeInfoSystem : ISystem
{
    private Dictionary<int, OperativeInfoCmponent> _components = new Dictionary<int, OperativeInfoCmponent>();

    public void AddComponent(Entity entity, ComponentBase component)
    {
        _components.Add(entity.Id, (OperativeInfoCmponent)component);
    }

    public bool IsProcessing()
    {
        return false;
    }

    public void Update()
    {
        
    }

    public void RemoveComponent(int entityId)
    {
        _components.Remove(entityId);
    }
}
