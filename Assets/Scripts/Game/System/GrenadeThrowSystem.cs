using System.Collections.Generic;

public class GrenadeThrowSystem : ISystem
{
    private readonly Dictionary<int, GrenadeThrowComponent> _components = new Dictionary<int, GrenadeThrowComponent>();

    public void Update()
    {
        foreach (var pair in _components)
        {
            var component = pair.Value;

            if (component.Target != null)
            {
                var entity = Game.I.EntityManager.GetEntity(pair.Key);
                var position = entity.GetEcsComponent<MovementComponent>().Position;
                Game.I.EntitySpawner.CreateGrenade(position, component.Target, component.Range);
                component.Target = null;
            }
        }
    }

    public void AddComponent(Entity entity, ComponentBase component)
    {
        _components.Add(entity.Id, (GrenadeThrowComponent)component);
    }

    public void RemoveComponent(int entityId)
    {
        _components.Remove(entityId);
    }

    public bool IsProcessing()
    {
        return false;
    }
}
