using System.Collections.Generic;

[Component(ComponentType.Movement)]
public class MovementComponent : ComponentBase
{
    public List<Point> Path;
    public Point Position;
    public int MoveLimit = 7;

    public MovementComponent(List<Point> path)
    {
        Path = path;
    }

    public MovementComponent(Point point)
    {
        Position = point;
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
