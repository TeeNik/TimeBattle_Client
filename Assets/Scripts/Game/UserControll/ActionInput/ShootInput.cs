using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootInput : ActionInput
{
    private PredictionMap _prediction;
    private List<Point> _range;

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
        _prediction.DrawCharacter();
        ShootComponent sc = new ShootComponent(_range);
        Game.I.InputController.ProduceInput(GetActionType(), sc);

        _range = null;
    }

    public void Update(Character ch)
    {
        var map = Game.I.MapController;
        var position = ch.GetComponent<PositionComponent>().Position;
        var tile = map.GetTileByMouse();
        Weapon weapon = ch.Weapon;

        List<Point> range;
        if (tile.y < position.Y)
        {
            range = weapon.Left;
        }
        else if (tile.y > position.Y)
        {
            range = weapon.Right;
        }
        else if (tile.x > position.X)
        {
            range = weapon.Down;
        }
        else
        {
            range = weapon.Up;
        }

        List<Point> toDraw = new List<Point>();
        foreach(var point in range)
        {
            toDraw.Add(position.Sum(point));
        }
        _range = toDraw;
        _prediction.DrawPath(toDraw);
    }
}
