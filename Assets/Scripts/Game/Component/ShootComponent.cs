using System.Collections.Generic;

public class ShootComponent : ComponentBase
{
    public readonly List<Point> Range;

    public ShootComponent(List<Point> range)
    {
        Range = range;
    }
}
