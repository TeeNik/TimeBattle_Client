using System.Collections.Generic;
using DG.Tweening;
using System.Linq;

public class MovementSystem : ISystem
{
    private int _moving = 0;

    private readonly Dictionary<int, MovementComponent> _components = new Dictionary<int, MovementComponent>();
    private readonly List<int> _toDelete = new List<int>();

    public void AddComponent(Entity entity, ComponentBase component)
    {
        var mc = (MovementComponent)component;
        _components.Add(entity.Id, mc);

        var pos = Game.I.MapController.SetToPosition(entity.Id, mc.MapType, mc.Position);
        entity.transform.position = pos;
    }

    public void Update()
    {
        var map = Game.I.MapController;

        foreach (var keyValuePair in _components)
        {
            if (_toDelete.Contains(keyValuePair.Key))
            {
                continue;
            }

            var component = keyValuePair.Value;
            var key = keyValuePair.Key;
            if (component.Path != null && component.Path.Count > 0)
            {
                var entity = Game.I.EntityManager.GetEntity(key);
                var nextPosition = component.Path.First();
                component.Path.Remove(nextPosition);
                var position = keyValuePair.Value.Position;
                var mapData = map.MapDatas[nextPosition.X][nextPosition.Y];
                var onMapType = component.MapType;
                if (mapData.Type != OnMapType.Empty)
                {
                    if (component.Path.Count == 0)
                    {
                        continue;
                    }
                    else
                    {
                        onMapType = mapData.Type;
                    }
                }

                var pos = map.MoveToPosition(key, onMapType, position, nextPosition);
                keyValuePair.Value.Position = nextPosition;

                ++_moving;
                component.IsMoving = true;
                entity.transform.DOMove(pos, 1f).SetSpeedBased().SetEase(Ease.Linear).OnComplete(()=>StopMoving(component));
            }
        }

        foreach (var comp in _toDelete)
        {
            _components.Remove(comp);
        }
        _toDelete.Clear();
    }

    private void StopMoving(MovementComponent mc)
    {
        mc.IsMoving = false;
        mc.OnEndMoving?.Invoke();
        --_moving;
    }

    public bool IsProcessing()
    {
        return _moving > 0;
    }

    public void RemoveComponent(int entityId)
    {
        Game.I.MapController.SetMapData(_components[entityId].Position, OnMapType.Empty);

        _toDelete.Add(entityId);
    }

    public int GetPhaseLegth()
    {
        var notNull = _components.Values.Where(c => c.Path != null).ToArray();
        return !notNull.Any() ? 0 : notNull.Max(c => c.Path.Count);
    }
}
