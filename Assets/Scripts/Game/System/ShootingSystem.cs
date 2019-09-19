﻿using System.Collections.Generic;
using System.Linq;

public class ShootingSystem : ISystem
{
    private Dictionary<int, ShootComponent> _components = new Dictionary<int, ShootComponent>();

    //TODO may be another way
    private readonly List<int> _toDelete = new List<int>();

    public void Update()
    {
        var map = Game.I.MapController;

        var msgs = new List<TakeDamageMsg>();

        foreach (var component in _components)
        {
            if (_toDelete.Contains(component.Key))
            {
                continue;
            }

            var range = component.Value.Range;

            if (range != null)
            {
                var entity = Game.I.EntityManager.GetEntity(component.Key);
                var info = entity.GetEcsComponent<OperativeInfoComponent>();
                var enemy = Utils.PlayerTypeToMap(Utils.GetOppositePlayer(info.Owner));
                foreach (var point in range)
                {
                    var hasEnemy = map.HasEnemy(point, enemy);

                    if (hasEnemy)
                    {
                        var target = map.CheckCover(range, point);
                        if(target.HasValue)
                        {
                            msgs.Add(new TakeDamageMsg(target.Value, 1));
                        }
                        range.Clear();
                        break;
                    }
                }
            }
        }

        foreach (var msg in msgs)
        {
            MakeShoot(msg);
        }

        foreach (var comp in _toDelete)
        {
            _components.Remove(comp);
        }
        _toDelete.Clear();
    }

    private void MakeShoot(TakeDamageMsg msg)
    {
        var target = Game.I.EntityManager.GetEntity(msg.EntityId);
        var mc = target.GetEcsComponent<MovementComponent>();
        if (mc.IsMoving)
        {
            mc.OnEndMoving += ()=> Game.I.Messages.SendEvent(msg);
        }
        else
        {
            Game.I.Messages.SendEvent(msg);
        }
    }

    public void AddComponent(Entity entity, ComponentBase component)
    {
        _components.Add(entity.Id, (ShootComponent)component);
    }

    public void OnUpdateEnd()
    {
        foreach(var comp in _components.Values.Where(c=>c.Range != null && c.Range.Count > 0))
        {
            comp.Range.Clear();
        }
    }

    public bool IsProcessing()
    {
        return false;
    }

    public void RemoveComponent(int entityId)
    {
        _toDelete.Add(entityId);
    }

    public int GetPhaseLegth()
    {
        return 1;
    }
}
