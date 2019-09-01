using System.Collections.Generic;

public class HealthSystem : ISystem
{
    private EventListener _eventListener;
    private Dictionary<int, HealthComponent> _components = new Dictionary<int, HealthComponent>();
    private Dictionary<int, HealthView> _views = new Dictionary<int, HealthView>();

    public HealthSystem()
    {
        _eventListener = new EventListener();
        _eventListener.Add(Game.I.Messages.Subscribe<TakeDamageMsg>(OnTakeDamageMsg));
    }

    private void OnTakeDamageMsg(TakeDamageMsg msg)
    {
        var id = msg.EntityId;
        var entity = (Character)Game.I.EntityManager.GetEntity(id);
        var hc = _components[id];
        --hc.CurrentHealth;

        if(hc.CurrentHealth == 0)
        {
            Game.I.EntityManager.DestroyEntity(id);
        }
        else
        {
            var view = _views[id];
            view.SetHealth((float)hc.CurrentHealth / hc.MaxHealth);
        }
    }

    public void Update()
    {
        
    }

    public void AddComponent(Entity entity, ComponentBase component)
    {
        var hc = (HealthComponent)component;
        _components.Add(entity.Id, hc);

        var view = entity.GetComponent<HealthView>();
        view.SetHealth((float)hc.CurrentHealth / hc.MaxHealth);
        _views.Add(entity.Id, view);
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
