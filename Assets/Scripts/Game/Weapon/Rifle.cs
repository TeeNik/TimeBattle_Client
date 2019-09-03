using System.Collections.Generic;

public class Rifle : Weapon
{
    public Rifle()
    {
        Type = WeaponType.Rifle;
    }

    protected override List<Point> Distance { get; } = new List<Point>
        {
            new Point(1, 0), new Point(2, 0), new Point(3, 0), new Point(4, 0),
            new Point(5, 0), new Point(6, 0),
        };
}
