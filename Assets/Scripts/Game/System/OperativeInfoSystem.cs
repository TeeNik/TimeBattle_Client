using System.Collections.Generic;

public class OperativeInfoSystem : ISystem
{
    private Dictionary<int, OperativeInfoComponent> _components = new Dictionary<int, OperativeInfoComponent>();

    public void AddComponent(Entity entity, ComponentBase component)
    {
        var ic = (OperativeInfoComponent) component;
        _components.Add(entity.Id, ic);

        entity.GetComponent<InfoView>().SetInfo(ic);
    }

    public bool IsProcessing()
    {
        return false;
    }

    public void Update()
    {
        
    }

    public List<int> GetEntitiesByOwner(PlayerType owner)
    {
        var list = new List<int>();
        foreach (var info in _components)
        {
            if (info.Value.Owner == owner)
            {
                list.Add(info.Key);
            }
        }
        return list;
    }

    public void RemoveComponent(int entityId)
    {
        _components.Remove(entityId);
    }
}
