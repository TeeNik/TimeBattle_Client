using System.Collections.Generic;
using DG.Tweening;
using System.Linq;

public class MovementSystem : ISystem
{
    private int _moving = 0;

    private Dictionary<int, MovementComponent> _components = new Dictionary<int, MovementComponent>();

    public void AddComponent(Entity entity, ComponentBase component)
    {
        var mc = (MovementComponent)component;
        _components.Add(entity.Id, mc);

        var info = entity.GetEcsComponent<OperativeInfoCmponent>();
        var mapData = info.Owner == PlayerType.Player1 ? MapData.Player1 : MapData.Player2;
        var pos = Game.I.MapController.SetToPosition(mapData, mc.Position);
        entity.transform.position = pos;
    }

    public void Update()
    {
        var map = Game.I.MapController;

        foreach (var component in _components)
        {
            var data = component.Value;
            if (data.Path != null && data.Path.Count > 0)
            {
                var entity = Game.I.EntityManager.GetEntity(component.Key);

                var nextPosition = data.Path.First();
                data.Path.Remove(nextPosition);

                var position = component.Value.Position;
                var info = entity.GetEcsComponent<OperativeInfoCmponent>();
                var mapData = info.Owner == PlayerType.Player1 ? MapData.Player1 : MapData.Player2;
                var pos = map.MoveToPosition(mapData, component.Value.Position, nextPosition);
                component.Value.Position = nextPosition;

                ++_moving;
                entity.transform.DOMove(pos, 1f).SetSpeedBased().SetEase(Ease.Linear).OnComplete(OnStopMoving);
            }

        }
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
        return _components.Any(c=>c.Value.Path != null);
    }

    public void RemoveComponent(int entityId)
    {
        _components.Remove(entityId);
    }
}
