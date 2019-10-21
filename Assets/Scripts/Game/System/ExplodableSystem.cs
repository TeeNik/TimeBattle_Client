using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ExplodableSystem : ISystem
{

    private readonly Dictionary<int, ExplodableComponent> _components = new Dictionary<int, ExplodableComponent>();
    private readonly List<int> _toDelete = new List<int>();
    private int _isMoving;

    public void Update()
    {
        var mapData = Game.I.MapController.MapDatas;
        foreach (var pair in _components)
        {
            if (_toDelete.Contains(pair.Key))
            {
                return;
            }

            var component = pair.Value;

            if (component.IsExloding)
            {
                foreach (var point in component.Range)
                {
                    var id = mapData[point.X][point.Y].EntityId;
                    if (id != null)
                    {
                        Game.I.Messages.SendEvent(new TakeDamageMsg(id.Value, 2));
                    }
                }
                var entity = Game.I.EntityManager.GetEntity(pair.Key);
                Game.I.EntityManager.DestroyEntity(pair.Key);
                Object.Destroy(entity.gameObject);
            }
            else
            {
                var entity = Game.I.EntityManager.GetEntity(pair.Key);
                var target = Game.I.MapController.GetTileWorldPosition(component.Target);
                entity.transform.DOMove(target, Utils.MovementSpeed).SetEase(Ease.Linear).OnComplete(OnMoveComplete);
                component.IsExloding = true;
            }
        }

        foreach (var i in _toDelete)
        {
            _components.Remove(i);
        }
        _toDelete.Clear();
    }

    private void Explode()
    {

    }

    private void OnMoveComplete()
    {
        --_isMoving;
    }

    public void AddComponent(Entity entity, ComponentBase component)
    {
        var ec = (ExplodableComponent) component;
        entity.transform.position = Game.I.MapController.GetTileWorldPosition(ec.StartPosition);
        _components.Add(entity.Id, ec);
    }

    public void RemoveComponent(int entityId)
    {
        _toDelete.Add(entityId);
    }

    public bool IsProcessing()
    {
        return _isMoving > 0;
    }
}
