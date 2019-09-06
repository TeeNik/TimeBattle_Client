using System.Collections.Generic;
using UnityEngine;

public class MoveInput : ActionInput
{
    private readonly PredictionMap _prediction;
    private Point _lastPoint;
    private readonly MapController _map;
    private Vector3 _pos;
    private Character _char;
    private List<Point> _path;

    public MoveInput(PredictionMap prediction)
    {
        _prediction = prediction;
        _map = Game.I.MapController;
    }

    public void ProduceInput()
    {
        _prediction.DrawCharacter(_char);
        MovementComponent mc = new MovementComponent(_path);
        Game.I.InputController.ProduceInput(GetActionType(), mc);

        _lastPoint = null;
        _path = null;
    }

    public void Start(Character ch)
    {
        _char = ch;
        _pos = ch.transform.position;
    }

    public void Update()
    {
        var tile = _map.GetTileByMouse();
        if (Game.I.MapController.IsWalkable(tile) )
        {
            if (_lastPoint == null || _lastPoint.X != tile.x || _lastPoint.Y != tile.y)
            {
                _lastPoint = new Point(tile.x, tile.y);
                var start = _map.GetTileByVector3(_pos);
                var startPoint = new Point(start.x, start.y);
                var path = _map.PathFinder.FindPath(startPoint, _lastPoint, false);
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
