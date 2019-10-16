using System.Collections.Generic;
using UnityEngine;

class Shotgun : Weapon
{
    public Shotgun() : base(WeaponType.Shotgun)
    {
        GenerateBasicDirections();
    }

    //Don't change order
    protected override List<Point> Distance { get; } = new List<Point>
    {
        new Point(1, 1), new Point(1, -1), new Point(-1, 1), new Point(-1, -1),
        new Point(1, 0), new Point(2, 0),   
        new Point(-1, 0), new Point(-2, 0), 
        new Point(0, 1), new Point(0, 2),
        new Point(0, -1), new Point(0, -2),
    };

    public override List<List<Point>> GetRanges()
    {
        return new List<List<Point>> { Distance };
    }

    public override IEnumerable<List<Point>> GetAvailableRange(Point position)
    {
        var availableRange = new List<List<Point>>();
        var list = new List<Point>();
        var map = Game.I.MapController;
        var mapData = map.MapDatas;
        for (int i = 0; i < Distance.Count; i++)
        {
            Point point = Distance[i];
            var p = position.Sum(point);
            if (!map.IsInBounds(p) || mapData[p.X][p.Y].Type == OnMapType.Wall)
            {
                if (point.X != 0 && point.Y != 0)
                {
                    continue;
                }
                else 
                {
                    var value = point.X + point.Y;
                    if (Mathf.Abs(value) == 1)
                    {
                        ++i;
                    }
                    continue;
                }
            }
            list.Add(p);
        }
        availableRange.Add(list);
        return availableRange;
    }
}
