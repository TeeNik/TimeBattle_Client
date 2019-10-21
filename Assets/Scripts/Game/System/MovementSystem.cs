using System.Collections.Generic;
using DG.Tweening;
using System.Linq;

public class MovementSystem : ISystem
{
    private int _moving = 0;

    private readonly Dictionary<int, MovementComponent> _components = new Dictionary<int, MovementComponent>();
    private readonly Dictionary<int, CharacterView> _views = new Dictionary<int, CharacterView>();
    private readonly List<int> _toDelete = new List<int>();

    public void AddComponent(Entity entity, ComponentBase component)
    {
        var mc = (MovementComponent)component;
        _components.Add(entity.Id, mc);

        var pos = Game.I.MapController.SetToPosition(entity.Id, mc.MapType, mc.Position);
        entity.transform.position = pos;
        _views.Add(entity.Id, entity.GetComponent<CharacterView>());
    }

    public void Update()
    {
        var map = Game.I.MapController;

        foreach (var pair in _components)
        {
            if (_toDelete.Contains(pair.Key))
            {
                continue;
            }

            var component = pair.Value;
            var id = pair.Key;
            if (component.Path != null && component.Path.Count > 0)
            {
                var entity = Game.I.EntityManager.GetEntity(id);
                var nextPosition = component.Path.First();
                component.Path.Remove(nextPosition);
                var position = pair.Value.Position;
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

                var pos = map.MoveToPosition(id, onMapType, position, nextPosition);
                _views[id].Rotate(position, nextPosition);
                pair.Value.Position = nextPosition;

                ++_moving;
                component.IsMoving = true;
                entity.transform.DOMove(pos, Utils.MovementSpeed).SetEase(Ease.Linear).OnComplete(()=>StopMoving(component));
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
        mc.OnEndMoving = null;
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
}
