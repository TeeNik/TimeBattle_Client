using System.Collections.Generic;

[Component(ComponentType.Shoot)]
public class ShootComponent : ComponentBase
{
    public List<Point> Range;
    public Weapon Weapon;

    public ShootComponent()
    {

    }

    public ShootComponent(Weapon weapon)
    {
        Weapon = weapon;
    }

    public ShootComponent(List<Point> range)
    {
        Range = range;
        Weapon = new Pistol();
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
