using System.Collections.Generic;

public class OperativeInfoSystem : ISystem
{
    private Dictionary<int, OperativeInfoCmponent> _components = new Dictionary<int, OperativeInfoCmponent>();

    public void AddComponent(int entityId, ComponentBase component)
    {
        _components.Add(entityId, (OperativeInfoCmponent)component);
    }

    public bool IsProcessing()
    {
        return false;
    }

    public void Update()
    {
        
    }
}
