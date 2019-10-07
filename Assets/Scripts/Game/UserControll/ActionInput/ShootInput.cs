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
            var list = new List<ShootComponent>{ new ShootComponent(_range), new ShootComponent(_range.ToList()) };
            Game.I.UserInputController.ProduceInput(GetActionType(), list);

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

        foreach (var range in _weapon.GetFullRange())
        {
            foreach (var point in range)
            {
                var p = _position.Sum(point);
                if (!map.IsInBounds(p) || mapData[p.X][p.Y].Type == OnMapType.Wall)
                {
                    break;
                }
                fullRange.Add(p);
            }
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
                var resultPoint = _position.Sum(p);
                if (fullRange.Any(r => r.Equals(resultPoint)))
                {
                    toDraw.Add(resultPoint);
                }
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
