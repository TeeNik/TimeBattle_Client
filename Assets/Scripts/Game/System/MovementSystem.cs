using System.Collections.Generic;
using DG.Tweening;
using System.Linq;

public class MovementSystem : ISystem
{
    private int _moving = 0;

    private Dictionary<int, MovementComponent> _components = new Dictionary<int, MovementComponent>();
    private readonly List<int> _toDelete = new List<int>();

    public void AddComponent(Entity entity, ComponentBase component)
    {
        var mc = (MovementComponent)component;
        _components.Add(entity.Id, mc);

        var info = entity.GetEcsComponent<OperativeInfoComponent>();
        var mapType = info.Owner == PlayerType.Player1 ? OnMapType.Player1 : OnMapType.Player2;
        var pos = Game.I.MapController.SetToPosition(entity.Id, mapType, mc.Position);
        entity.transform.position = pos;
    }

    public void Update()
    {
        var map = Game.I.MapController;

        foreach (var component in _components)
        {
            if (_toDelete.Contains(component.Key))
            {
                continue;
            }

            var data = component.Value;
            if (data.Path != null && data.Path.Count > 0)
            {
                var entity = Game.I.EntityManager.GetEntity(component.Key);

                var nextPosition = data.Path.First();
                data.Path.Remove(nextPosition);

                var position = component.Value.Position;
                var info = entity.GetEcsComponent<OperativeInfoComponent>();
                var mapData = info.Owner == PlayerType.Player1 ? OnMapType.Player1 : OnMapType.Player2;
                var pos = map.MoveToPosition(component.Key ,mapData, position, nextPosition);
                component.Value.Position = nextPosition;

                ++_moving;
                entity.transform.DOMove(pos, 1f).SetSpeedBased().SetEase(Ease.Linear).OnComplete(OnStopMoving);
            }

        }

        foreach (var comp in _toDelete)
        {
            _components.Remove(comp);
        }
        _toDelete.Clear();
    }

    private void OnStopMoving()
    {
        --_moving;
        if(_moving == 0)
        {
            Game.I.IterateOverPhase();
        }
    }

    public bool IsProcessing()
    {
        return _components.Any(c => c.Value.Path != null && c.Value.Path.Count > 0);
    }

    public void RemoveComponent(int entityId)
    {
        _toDelete.Add(entityId);
    }
}
