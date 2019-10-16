using System.Collections.Generic;

public class GrenadeWeapon : Weapon
{
    private readonly List<List<Point>> _ranges;
    private const int Range = 4;

    private readonly List<Point> _direction = new List<Point>()
    {
        new Point(-1, 1),
        new Point(0, 1),
        new Point(1, 1),
        new Point(-1, 0),
        new Point(1, 0),
        new Point(-1, -1),
        new Point(0, -1),
        new Point(-1, 1),
    };

    public GrenadeWeapon() : base(WeaponType.Grenade)
    {

        _ranges = new List<List<Point>>();
        for (int i = 0; i < 8; i++)
        {
            _ranges.Add(new List<Point>());

            var dir = _direction[i];
            for (int j = 1; j < Range + 1; j++)
            {
                _ranges[i].Add(new Point(dir.X * j, dir.Y * j));
            }
        }
    }

    protected override List<Point> Distance { get; }

    public override List<List<Point>> GetRanges()
    {
        return _ranges;
    }
}
