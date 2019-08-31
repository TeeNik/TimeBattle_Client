using System.Collections.Generic;

public class HealthSystem : ISystem
{
    private EventListener _eventListener;
    private Dictionary<int, HealthComponent> _components = new Dictionary<int, HealthComponent>();

    public HealthSystem()
    {
        _eventListener = new EventListener();
        _eventListener.Add(Game.I.Messages.Subscribe<TakeDamageMsg>(OnTakeDamageMsg));
    }

    private void OnTakeDamageMsg(TakeDamageMsg msg)
    {
        var entity = (Character)Game.I.EntityManager.GetEntity(msg.EntityId);
    }

    public void Update()
    {
        
    }

    public void AddComponent(Entity entity, ComponentBase component)
    {
        _components.Add(entity.Id, (HealthComponent)component);
    }

    public bool IsProcessing()
    {
        return false;
    }

    public void RemoveComponent(int entityId)
    {
        _components.Remove(entityId);
    }
}
