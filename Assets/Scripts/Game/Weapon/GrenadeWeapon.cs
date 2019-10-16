using System.Collections.Generic;

public class GrenadeWeapon : Weapon
{
    private readonly List<List<Point>> _ranges;
    private const int Range = 4;

    private readonly List<Point> _direction = new List<Point>()
    {
        new Point(-1, 1), new Point(0, 1), new Point(1, 1),
        new Point(-1, 0), new Point(0, 0), new Point(1, 0),
        new Point(-1, -1), new Point(0, -1), new Point(1, -1),
    };


    public GrenadeWeapon() : base(WeaponType.Grenade)
    {
        _ranges = new List<List<Point>>();
        for (int i = 0; i < _direction.Count; i++)
        {
            _ranges.Add(new List<Point>());

            var dir = _direction[i];
            for (int j = 1; j < Range + 1; j++)
            {
                _ranges[i].Add(new Point(dir.X * j, dir.Y * j));
            }
        }
    }

    public List<Point> GetExplosionRadius(Point position)
    {
        var map = Game.I.MapController;
        var list = new List<Point>();
        foreach (var point in _direction)
        {
            var p = position.Sum(point);
            if (map.IsNotWall(p))
            {
                list.Add(p);
            }
        }
        return list;
    }

    protected override List<Point> Distance => _direction;

    public override List<List<Point>> GetRanges()
    {
        return _ranges;
    }
}
