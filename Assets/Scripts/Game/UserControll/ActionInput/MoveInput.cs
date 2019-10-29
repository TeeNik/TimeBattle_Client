using System;
using System.Collections.Generic;
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
    private readonly Action _show;
    private readonly Action _hide;

    public MoveInput(Action show, Action hide)
    {
        _prediction = Game.I.UserInputController.ActionController.PredictionMap;
        _map = Game.I.MapController;
        _show = show;
        _hide = hide;
    }

    public void ProduceInput()
    {
        if(_path != null && _path.Count > 0)
        {
            _prediction.DrawCharacter(_char, _path.Last());
            _prediction.DrawMovePath(_path);
            var mc = new MovementComponent(_path);
            Game.I.UserInputController.ProduceInput(GetActionType(), mc);

            _lastPoint = null;
            _path = null;
        }
        Game.I.MapController.OutlinePool.ReturnAll();
    }

    public void WaitForConfirm()
    {
        if (_path != null && _path.Count > 0)
        {
            _prediction.DrawMoveInput(_path);
            _show();
        }
        else
        {
            _hide();
        }
    }

    public void Start(Character ch)
    {
        _char = ch;
        _pos = ch.transform.position;
        _moveLimit = ch.GetEcsComponent<CharacterActionComponent>().Energy;
        DrawArea();
    }

    public void DrawArea()
    {
        var map = Game.I.MapController;
        var data = map.MapDatas;
        var pos = map.GetTileByVector3(_pos);
        var x = pos.x;
        var y = pos.y;
        var r = _moveLimit;
        for (int i = 0; i < data.Length; i++)
        {
            for (int j = 0; j < data[i].Length; j++)
            {
                var value = Mathf.Abs(i - x) + Mathf.Abs(j - y);
                if (value <= r && map.IsWalkable(new Vector3Int(i, j, 0)))
                {
                    var point = new Point(i, j);
                    var start = new Point(pos.x,pos.y);
                    var path = map.PathFinder.FindPath(start, point, false);
                    if (path != null && path.Count <= r)
                    {
                        var o = map.OutlinePool.GetFromPool();
                        o.transform.position = Game.I.MapController.GetTileWorldPosition(new Point(i, j));
                    }
                }
            }
        }
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
                    path.RemoveRange(_moveLimit, path.Count - _moveLimit);
                }
                _path = path;
                _prediction.DrawMoveInput(path);
            }
        }
        else
        {
            _lastPoint = null;
            _path = null;
            _prediction.ClearLayer(Layers.Temporary);
        }

    }

    public ActionType GetActionType()
    {
        return ActionType.Move;
    }
}
