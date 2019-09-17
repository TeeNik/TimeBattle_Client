using System.Collections.Generic;

public class FlagCarringSystem : ISystem
{
    private readonly Dictionary<int, FlagCarryComponent> _components = new Dictionary<int, FlagCarryComponent>();

    public void Update()
    {
        foreach (var component in _components)
        {
            var entity = Game.I.EntityManager.GetEntity(component.Key);

            var mc = entity.GetEcsComponent<MovementComponent>();
        }
    }

    public void AddComponent(Entity entity, ComponentBase component)
    {
        _components.Add(entity.Id, (FlagCarryComponent)component);
    }

    public void RemoveComponent(int entityId)
    {
    }

    public bool IsProcessing()
    {
        return false;
    }

    public int GetPhaseLegth()
    {
        return 1;
    }
}
