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
        Debug.Log(Time.time);
        var map = Game.I.MapController;

        var position = Game.I.SystemController.PositionSystem.GetComponent(ch.Id).Position;

        var tile = map.GetTileByMouse();

        List<Point> range = null;
        Weapon weapon = ch.Weapon;

        if(tile.y < position.Y)
        {
            Debug.Log("Left");
            range = weapon.Left;
        }
        else if (tile.y > position.Y)
        {
            Debug.Log("Right");
            range = weapon.Right;
        } 
        else if (tile.x > position.X)
        {
            Debug.Log("Down");
            range = weapon.Down;
        }
        else
        {
            Debug.Log("Up");
            range = weapon.Up;
        }

        List<Point> toDraw = new List<Point>();
        foreach(var point in range)
        {
            toDraw.Add(position.Sum(point));
        }

        _prediction.DrawPath(toDraw);
    }
}
