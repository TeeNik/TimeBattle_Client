﻿using System.Collections.Generic;

public class ShootingSystem : ISystem
{
    private Dictionary<int, ShootComponent> _components = new Dictionary<int, ShootComponent>();

    //TODO may be another way
    private List<int> ToDelete = new List<int>();

    public void Update()
    {
        var map = Game.I.MapController;

        foreach (var component in _components)
        {
            if (ToDelete.Contains(component.Key))
            {
                continue;
            }

            var range = component.Value.Range;

            if (range != null)
            {
                var entity = Game.I.EntityManager.GetEntity(component.Key);
                var info = entity.GetEcsComponent<OperativeInfoCmponent>();
                var enemy = Utils.PlayerTypeToMap(Utils.GetOppositePlayer(info.Owner));
                foreach (var point in range)
                {
                    var hasEnemy = map.HasEnemy(point, enemy);

                    if (hasEnemy)
                    {
                        var enemyId = map.GetEntityByPoint(point).Value;
                        Game.I.Messages.SendEvent<TakeDamageMsg>(new TakeDamageMsg(enemyId, 1));
                        break;
                    }
                }
            }
        }

        foreach(var comp in ToDelete)
        {
            _components.Remove(comp);
        }
        ToDelete.Clear();
    }

    public void AddComponent(Entity entity, ComponentBase component)
    {
        _components.Add(entity.Id, (ShootComponent)component);
    }

    public bool IsProcessing()
    {
        return false;
    }

    public void RemoveComponent(int entityId)
    {
        ToDelete.Add(entityId);
    }
}