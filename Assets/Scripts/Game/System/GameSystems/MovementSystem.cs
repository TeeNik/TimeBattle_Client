using DG.Tweening;
using System.Linq;

public class MovementSystem : GameSystem<MovementComponent>
{
    private int _moving = 0;

    public override void UpdateImpl()
    {
        foreach (var component in Components)
        {
            var entity = Game.I.EntityManager.GetEntity(component.Key);
            var data = component.Value;
            var map = Game.I.MapController;

            var nextPosition = data.Positions.First();
            data.Positions.Remove(nextPosition);

            var system = Game.I.SystemController;
            var position = system.PositionSystem.GetComponent(entity.Id);

            var pos = map.MoveToPosition(MapData.Player, position.Position, nextPosition);
            position.Position = nextPosition;

            ++_moving;
            entity.transform.DOMove(pos, 1f).SetSpeedBased().SetEase(Ease.Linear).OnComplete(OnStopMoving);

            if (data.Positions.Count == 0)
            {
                ToDelete.Add(component.Key);
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

    public override bool IsProcessing()
    {
        return Components.Count > 0;
    }
}
