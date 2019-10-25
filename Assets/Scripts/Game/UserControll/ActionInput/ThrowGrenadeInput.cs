using System;
using System.Collections.Generic;
using System.Linq;

public class ThrowGrenadeInput : ActionInput
{

    private GrenadeWeapon _grenade = new GrenadeWeapon();
    private Point _position;
    private readonly PredictionMap _prediction;
    private Point _target;
    private readonly Action _show;
    private readonly Action _hide;

    public ActionType GetActionType()
    {
        return ActionType.ThrowGrenade;
    }

    public ThrowGrenadeInput(Action show, Action hide)
    {
        _prediction = Game.I.UserInputController.ActionController.PredictionMap;
        _show = show;
        _hide = hide;
    }

    public void Update()
    {
        var tile = Game.I.MapController.GetTileByMouse();
        var mousePoint = new Point(tile.x, tile.y);

        foreach (var range in _grenade.GetAvailableRange(_position))
        {
            if (range.Any(r => r.Equals(mousePoint)))
            {
                _target = mousePoint;
                DrawExplosionRange(mousePoint);
                break;
            }
            _target = null;
            _prediction.ClearLayer(Layers.Temporary);
        }
    }

    public void ProduceInput()
    {
        var range = _grenade.GetExplosionRadius(_target);
        var comp = new GrenadeThrowComponent(_target, range);
        Game.I.UserInputController.ProduceInput(GetActionType(), comp);
        _prediction.DrawShootingRange(range);
        _target = null;
        _hide();
    }

    private void DrawExplosionRange(Point target)
    {
        _prediction.DrawShootInput(_grenade.GetExplosionRadius(target));
    }

    public void WaitForConfirm()
    {
        if (_target != null)
        {
            DrawExplosionRange(_target);
            _show();
        }
        else
        {
            _hide();
        }
        Game.I.MapController.OutlinePool.ReturnAll();
    }

    public void Start(Character ch)
    {
        var tile = Game.I.MapController.GetTileByVector3(ch.transform.position);
        _position = new Point(tile.x, tile.y);
        DrawRanges();
    }

    private void DrawRanges()
    {
        var pool = Game.I.MapController.OutlinePool;
        var fullRange = new List<Point>();

        foreach (var range in _grenade.GetAvailableRange(_position))
        {
            fullRange.AddRange(range);
        }
        foreach (var point in fullRange)
        {
            var obj = pool.GetFromPool();
            obj.transform.position = Game.I.MapController.GetTileWorldPosition(point);
        }
    }
}
