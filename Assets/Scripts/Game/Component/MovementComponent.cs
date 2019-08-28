using System.Collections.Generic;

public class MovementComponent : ComponentBase
{
    public List<Point> Path;
    public Point Position;

    public void Update(MovementComponent data)
    {
        if (data.Position != null)
        {
            Position = data.Position;
        }
        if (data.Path != null)
        {
            Path = data.Path;
        }
    }
}
