using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : SystemBase<HealthComponent>
{
    private EventListener _eventListener;

    public HealthSystem()
    {
        _eventListener = new EventListener();
        _eventListener.Add(Game.I.Messages.Subscribe<TakeDamageMsg>(OnTakeDamageMsg));
    }

    private void OnTakeDamageMsg(TakeDamageMsg msg)
    {
        var entity = (Character)Game.I.EntityManager.GetEntity(msg.EntityId);

    }

    public override void UpdateImpl()
    {
    }

    public override void OnComponentAdded(int entityId, HealthComponent component)
    {
        base.OnComponentAdded(entityId, component);

        var entity = Game.I.EntityManager.GetEntity(entityId);
    }
}
