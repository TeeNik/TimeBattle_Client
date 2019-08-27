using System.Collections.Generic;

public class MovementComponent : ComponentBase
{
    public List<Point> Path;
    public Point Position;

    public void Update(ComponentBase data)
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
