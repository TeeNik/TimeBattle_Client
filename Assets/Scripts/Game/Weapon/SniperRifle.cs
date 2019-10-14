using System.Collections.Generic;

class SniperRifle : Weapon
{
    public SniperRifle() : base(WeaponType.Sniper)
    {
    }

    public override List<Point> Up { get; protected set; } = new List<Point>{ new Point(-1, 1), new Point(-2, 2), new Point(-3, 3), new Point(-4, 4), new Point(-5, 5) };
    public override List<Point> Right { get; protected set; } = new List<Point>{ new Point(1, 1), new Point(2, 2), new Point(3, 3), new Point(4, 4), new Point(5, 5) };
    public override List<Point> Down { get; protected set; } = new List<Point>{ new Point(1, -1), new Point(2, -2), new Point(3, -3), new Point(4, -4), new Point(5, -5) };
    public override List<Point> Left { get; protected set; } = new List<Point>{ new Point(-1, -1), new Point(-2, -2), new Point(-3, -3), new Point(-4, -4), new Point(-5, -5) };

    protected override List<Point> Distance { get; }

    public override List<List<Point>> GetRanges()
    {
        return new List<List<Point>> { Left, Down, Right, Up };

    }
}
