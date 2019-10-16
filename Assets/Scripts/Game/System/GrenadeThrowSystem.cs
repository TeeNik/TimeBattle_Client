using System.Collections.Generic;

public class GrenadeThrowSystem : ISystem
{
    private readonly Dictionary<int, GrenadeThrowComponent> _components = new Dictionary<int, GrenadeThrowComponent>();

    public void Update()
    {

        foreach (var pair in _components)
        {
            var component = pair.Value;
            var entity = Game.I.EntityManager.GetEntity(pair.Key);
            var position = entity.GetEcsComponent<MovementComponent>().Position;
            Game.I.EntitySpawner.CreateGrenade(position, component.Target);
        }

        _components.Clear();
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
