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
            _prediction.DrawPath(_range);
            ShootComponent sc = new ShootComponent(_range);
            Game.I.UserInputController.ProduceInput(GetActionType(), sc);

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

        foreach (var point in _weapon.GetFullRange())
        {
            fullRange.Add(_position.Sum(point));
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
        var point = new Point(tile.x, tile.y).Substract(_position);


        List<Point> range = null;
        if (_weapon.Left.Any(t=> point.Equals(t)))
        {
            range = _weapon.Left;
        }
        else if (_weapon.Right.Any(t => point.Equals(t)))
        {
            range = _weapon.Right;
        }
        else if (_weapon.Down.Any(t => point.Equals(t)))
        {
            range = _weapon.Down;
        }
        else if(_weapon.Up.Any(t => point.Equals(t)))
        {
            range = _weapon.Up;
        }

        if (range != null)
        {
            List<Point> toDraw = new List<Point>();
            foreach (var p in range)
            {
                toDraw.Add(_position.Sum(p));
            }
            _range = toDraw;
            _prediction.DrawPath(toDraw);
        }
        else
        {
            _range = null;
            _prediction.ClearTiles();
        }

    }
}
