using System.Collections.Generic;

class SniperRifle : Weapon
{
    public SniperRifle()
    {
        Type = WeaponType.Sniper;
    }

    protected override List<Point> Distance { get; } = new List<Point>
    {
        new Point(1, 1), new Point(2, 2), new Point(3, 3), new Point(4, 4), new Point(5, 5),
        //new Point(-1, 1), new Point(-2, 2), new Point(-3, 3), new Point(-4, 4), new Point(-5, 5),
        //new Point(1, -1), new Point(2, -2), new Point(3, -3), new Point(4, -4), new Point(5, -5),
        //new Point(-1, -1), new Point(-2, -2), new Point(-3, -3), new Point(-4, -4), new Point(-5, -5),
    };
}
