using System.Collections.Generic;

class Shotgun : Weapon
{
    public Shotgun() : base(WeaponType.Shotgun)
    {
        GenerateBasicDirections();
    }

    protected override List<Point> Distance { get; } = new List<Point>
    {
        new Point(1, 0), new Point(2, 0), new Point(3, 0),
        new Point(2, 1), new Point(2, -1),
        new Point(3, 1), new Point(3, -1),
    };
}
