﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveInput : ActionInput
{
    private readonly PredictionMap _prediction;
    private Point _lastPoint;
    private readonly MapController _map;
    private Vector3 _pos;
    private Character _char;
    private int _moveLimit;
    private List<Point> _path;

    public MoveInput(PredictionMap prediction)
    {
        _prediction = prediction;
        _map = Game.I.MapController;
    }

    public void ProduceInput()
    {
        if(_path != null)
        {
            _prediction.DrawCharacter(_char, _path.Last());
            MovementComponent mc = new MovementComponent(_path);
            Game.I.InputController.ProduceInput(GetActionType(), mc);

            _lastPoint = null;
            _path = null;
        }
    }

    public void Start(Character ch)
    {
        _char = ch;
        _pos = ch.transform.position;
        _moveLimit = ch.GetEcsComponent<MovementComponent>().MoveLimit;
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
                if(path.Count > _moveLimit)
                {
                    path.RemoveRange(_moveLimit, path.Count - _moveLimit - 1);
                }
                _path = path;
                _prediction.DrawPath(path);
            }

        }
        else
        {
            _lastPoint = null;
            _path = null;
            _prediction.ClearTiles();
        }

    }

    public ActionType GetActionType()
    {
        return ActionType.Move;
    }
}
