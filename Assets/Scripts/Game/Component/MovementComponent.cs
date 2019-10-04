using System;
using System.Collections.Generic;

[Component(ComponentType.Movement)]
public class MovementComponent : ComponentBase
{
    public OnMapType MapType;
    public List<Point> Path;
    public Point Position;
    public int MoveLimit = 7;

    public bool IsMoving;
    public Action OnEndMoving;

    public MovementComponent()
    {

    }

    public MovementComponent(List<Point> path)
    {
        Path = path;
    }

    public MovementComponent(Point point, OnMapType mapType)
    {
        Position = point;
        MapType = mapType;
    }

    public override void Update(ComponentBase data)
    {
        var mc = (MovementComponent)data;
        if (mc.Position != null)
        {
            Position = mc.Position;
        }
        if (mc.Path != null)
        {
            Path = mc.Path;
        }
    }
}
