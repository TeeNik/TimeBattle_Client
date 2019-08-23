using System.Collections.Generic;
using UnityEngine;

public class MoveInput : ActionInput
{
    private PredictionMap _prediction;
    private Point _lastPoint;
    private readonly MapController _map;
    private List<Point> _path;

    public MoveInput(PredictionMap prediction)
    {
        _prediction = prediction;
    }

    public void ProduceInput()
    {
        _prediction.DrawCharacter();
        MovementComponent mc = new MovementComponent(false, _path);
        Game.I.InputController.ProduceInput(GetActionType(), mc);

        _lastPoint = null;
        _path = null;
    }

    public void Update(Character ch)
    {
        var tile = Game.I.MapController.GetTileByMouse();
        var point = new Point(tile.x, tile.y);
        if (Game.I.MapController.IsWalkable(tile) )
        {
            if ((_lastPoint == null || !_lastPoint.Equals(point)))
            {
                _lastPoint = point;
                var start = Game.I.MapController.GetTileByVector3(ch.transform.position);
                var startPoint = new Point(start.x, start.y);
                var path = Game.I.MapController.PathFinder.FindPath(startPoint, point, false);
                _path = path;
                _prediction.DrawPath(path);
            }

        }
        else
        {
            _lastPoint = null;
            _prediction.ClearTiles();
        }

    }

    public ActionType GetActionType()
    {
        return ActionType.Move;
    }
}
