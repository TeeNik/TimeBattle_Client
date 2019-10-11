using System.Collections.Generic;
using System.Linq;

public class ShootInput : ActionInput
{
    private readonly PredictionMap _prediction;
    private List<Point> _range;

    private Character _char;
    private Point _position;
    private Weapon _weapon;

    public ActionType GetActionType()
    {
        return ActionType.Shoot;
    }

    public ShootInput(PredictionMap prediction)
    {
        _prediction = prediction;
    }

    public void ProduceInput()
    {
        if (_range != null)
        {
            _prediction.DrawShootingRange(_range);
            var comp = new ShootComponent(_range);
            Game.I.UserInputController.ProduceInput(GetActionType(), comp);

            _range = null;
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

    private List<Point> fullRange;

    private void DrawRanges()
    {
        var pool = Game.I.MapController.OutlinePool;
        fullRange = new List<Point>();
        var map = Game.I.MapController;
        var mapData = map.MapDatas;

        foreach (var range in _weapon.GetAvailableRange(_position))
        {
            fullRange.AddRange(range);
        }

        foreach (var point in fullRange)
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
