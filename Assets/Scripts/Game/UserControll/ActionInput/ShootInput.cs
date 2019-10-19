using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShootInput : ActionInput
{
    private readonly PredictionMap _prediction;
    private List<Point> _range;

    private const int MinDuration = 3;

    private Character _char;
    private Point _position;
    private Weapon _weapon;

    private readonly Action<int, int> _show;
    private readonly Action _hide;
    private readonly Func<int> _getDuration;

    public ActionType GetActionType()
    {
        return ActionType.Shoot;
    }

    public ShootInput(Action<int, int> show, Action hide, Func<int> getDuration)
    {
        _prediction = Game.I.UserInputController.ActionController.PredictionMap;
        _show = show;
        _hide = hide;
        _getDuration = getDuration;
    }

    public void ProduceInput()
    {
        var duration = _getDuration();
        var comp = new ShootComponent(_range, duration);
        Game.I.UserInputController.ProduceInput(GetActionType(), comp);
        _range = null;
        _hide();
    }

    public void WaitForConfirm()
    {
        if (_range != null)
        {
            _prediction.DrawShootInput(_range);
            var charInfo = _char.GetEcsComponent<CharacterActionComponent>();
            _show(Mathf.Min(charInfo.Energy, MinDuration), charInfo.Energy);
        }

        Game.I.MapController.OutlinePool.ReturnAll();
    }

    public void Start(Character ch)
    {
        _char = ch;
        _weapon = ch.GetEcsComponent<ShootComponent>().Weapon;
        var tile = Game.I.MapController.GetTileByVector3(ch.transform.position);
        _position = new Point(tile.x, tile.y);
        DrawRanges();
    }

    private List<Point> _fullRange;

    private void DrawRanges()
    {
        var pool = Game.I.MapController.OutlinePool;
        _fullRange = new List<Point>();

        foreach (var range in _weapon.GetAvailableRange(_position))
        {
            _fullRange.AddRange(range);
        }

        foreach (var point in _fullRange)
        {
            var obj = pool.GetFromPool();
            obj.transform.position = Game.I.MapController.GetTileWorldPosition(point);
        }
    }

    public void Update()
    {
        var map = Game.I.MapController;
        var tile = map.GetTileByMouse();
        var mousePoint = new Point(tile.x, tile.y);

        foreach (var range in _weapon.GetAvailableRange(_position))
        {
            if (range.Any(r => r.Equals(mousePoint)))
            {
                _range = range;
                _prediction.DrawShootInput(range);
                break;
            }
            _range = null;
            _prediction.ClearLayer(Layers.Temporary);
        }
    }
}
