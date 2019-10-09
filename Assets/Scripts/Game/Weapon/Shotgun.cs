using System.Collections.Generic;

class Shotgun : Weapon
{
    public Shotgun() : base(WeaponType.Shotgun)
    {
        GenerateBasicDirections();
    }

    protected override List<Point> Distance { get; } = new List<Point>
    {
        new Point(1, 0), new Point(2, 0), new Point(1, 1), new Point(1, -1),
        new Point(-1, 0), new Point(-2, 0), new Point(-1, 1), new Point(-1, -1),
        new Point(0, 1), new Point(0, 2),
        new Point(0, -1), new Point(0, -2),
    };

    public override List<List<Point>> GetRanges()
    {
        return new List<List<Point>> { Distance };

    }
}
