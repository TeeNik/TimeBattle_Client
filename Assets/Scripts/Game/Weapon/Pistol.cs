using System.Collections.Generic;

public class Pistol : Weapon
{
    public Pistol()
    {
        Type = WeaponType.Pistol;
    }
    protected override List<Point> Distance { get; } = new List<Point> { new Point(1, 0), new Point(2, 0), new Point(3, 0), new Point(4, 0) };
}
