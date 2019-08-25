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
        MovementComponent mc = new MovementComponent(_path);
        Game.I.InputController.ProduceInput(GetActionType(), mc);

        _lastPoint = null;
        _path = null;
    }

    public void Update(Character ch)
    {
        var tile = Game.I.MapController.GetTileByMouse();
        if (Game.I.MapController.IsWalkable(tile) )
        {
            if (_lastPoint == null || _lastPoint.X != tile.x || _lastPoint.Y != tile.y)
            {
                _lastPoint = new Point(tile.x, tile.y);
                var start = Game.I.MapController.GetTileByVector3(ch.transform.position);
                var startPoint = new Point(start.x, start.y);
                var path = Game.I.MapController.PathFinder.FindPath(startPoint, _lastPoint, false);
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
