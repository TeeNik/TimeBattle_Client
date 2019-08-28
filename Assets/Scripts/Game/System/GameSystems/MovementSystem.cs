using System.Collections.Generic;
using DG.Tweening;
using System.Linq;

public class MovementSystem : ISystem
{
    private int _moving = 0;

    private Dictionary<int, MovementComponent> _components = new Dictionary<int, MovementComponent>();

    public void AddComponent(int entityId, ComponentBase component)
    {
        _components.Add(entityId, (MovementComponent) component);
    }

    public void Update()
    {
        foreach (var component in _components)
        {
            var entity = Game.I.EntityManager.GetEntity(component.Key);
            var data = component.Value;
            var map = Game.I.MapController;

            var nextPosition = data.Path.First();
            data.Path.Remove(nextPosition);

            var position = component.Value.Position;
            var info = entity.GetComponent<OperativeInfoCmponent>();
            var mapData = info.Owner == PlayerType.Player1 ? MapData.Player1 : MapData.Player2;
            var pos = map.MoveToPosition(mapData, position, nextPosition);
            position = nextPosition;

            ++_moving;
            entity.transform.DOMove(pos, 1f).SetSpeedBased().SetEase(Ease.Linear).OnComplete(OnStopMoving);
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
        return _components.Count > 0;
    }


}
