using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : ISystem
{
    private Dictionary<int, ShootComponent> _components = new Dictionary<int, ShootComponent>();
    public void Update()
    {
        var map = Game.I.MapController;

        foreach (var component in _components)
        {
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
        _components.Remove(entityId);
    }
}
