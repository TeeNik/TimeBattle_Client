using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : SystemBase<HealthComponent>
{
    public override void UpdateImpl()
    {
    }

    public override void OnComponentAdded(int entityId, HealthComponent component)
    {
        base.OnComponentAdded(entityId, component);

        var entity = Game.I.EntityManager.GetEntity(entityId);
    }
}
