using System.Collections.Generic;

public class Pistol : Weapon
{
    private List<Point> _dist = new List<Point> { new Point(1, 0), new Point(2, 0), new Point(3, 0), new Point(4, 0) };
    protected override List<Point> Distance => _dist;

}
