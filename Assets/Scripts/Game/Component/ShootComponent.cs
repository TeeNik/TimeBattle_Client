using System.Collections.Generic;

[Component(ComponentType.Shoot)]
public class ShootComponent : ComponentBase
{
    public List<Point> Range;
    public Weapon Weapon;

    public ShootComponent(List<Point> range)
    {
        Range = range;
        Weapon = new SniperRifle();
    }

    public override void Update(ComponentBase newData)
    {
        var sc = (ShootComponent)newData;
        if(sc.Range != null)
        {
            Range = sc.Range;
        }
    }
}
