using System.Collections.Generic;

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
        _prediction.DrawPath(_range);
        ShootComponent sc = new ShootComponent(_range);
        Game.I.InputController.ProduceInput(GetActionType(), sc);

        _range = null;
    }

    public void Start(Character ch)
    {
        _char = ch;
        _weapon = ch.GetEcsComponent<ShootComponent>().Weapon;
        var tile = Game.I.MapController.GetTileByVector3(ch.transform.position);
        _position = new Point(tile.x, tile.y);
    }

    public void Update()
    {
        var map = Game.I.MapController;
        var tile = map.GetTileByMouse();

        List<Point> range;
        if (tile.y < _position.Y)
        {
            range = _weapon.Left;
        }
        else if (tile.y > _position.Y)
        {
            range = _weapon.Right;
        }
        else if (tile.x > _position.X)
        {
            range = _weapon.Down;
        }
        else
        {
            range = _weapon.Up;
        }

        List<Point> toDraw = new List<Point>();
        foreach(var point in range)
        {
            toDraw.Add(_position.Sum(point));
        }
        _range = toDraw;
        _prediction.DrawPath(toDraw);
    }
}
