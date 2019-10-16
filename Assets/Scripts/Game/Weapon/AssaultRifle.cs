using System.Collections.Generic;

public class AssaultRifle : Weapon
{
    public AssaultRifle() : base(WeaponType.Assault)
    {
        GenerateBasicDirections();
    }

    protected override List<Point> Distance { get; } = new List<Point>
        {
            new Point(1, 0), new Point(2, 0), new Point(3, 0), new Point(4, 0),
            new Point(5, 0), new Point(6, 0),
        };

    public override List<List<Point>> GetRanges()
    {
        return new List<List<Point>> { Left, Down, Right, Up };

    }
}
