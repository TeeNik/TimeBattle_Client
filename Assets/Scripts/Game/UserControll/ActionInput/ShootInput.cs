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

        foreach (var range in _weapon.GetRanges())
        {
            foreach (var point in range)
            {
                var p = _position.Sum(point);
                /*if (!map.IsInBounds(p) || mapData[p.X][p.Y].Type == OnMapType.Wall)
                {
                    break;
                }*/
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
        var mousePoint = new Point(tile.x, tile.y).Substract(_position);


        /*List<Point> range = null;
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
        }*/


        foreach (var range in _weapon.GetRanges())
        {
            if (range.Any(r => r./*Sum(r).*/Equals(mousePoint)))
            {
                List<Point> toDraw = new List<Point>();
                foreach (var point in range)
                {
                    toDraw.Add(_position.Sum(point));    
                }

                _range = toDraw;
                _prediction.DrawShootInput(toDraw);
                break;
            }
            _range = null;
            _prediction.ClearLayer(Layers.Temporary);
        }

        /*if (range != null)
        {
            foreach (var p in range)
            {
                var resultPoint = _position.Sum(p);
                if (fullRange.Any(r => r.Equals(resultPoint)))
                {
                    toDraw.Add(resultPoint);
                }
            }
            _range = toDraw;
            _prediction.DrawShootInput(toDraw);
        }
        else
        {
            _range = null;
            _prediction.ClearLayer(Layers.Temporary);
        }*/

    }
}
