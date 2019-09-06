using System.Collections.Generic;

public class ShootComponent : ComponentBase
{
    public List<Point> Range;
    public Weapon Weapon;

    public ShootComponent(List<Point> range)
    {
        Range = range;
        Weapon = new Pistol();
    }

    public void Update(ComponentBase newData)
    {
        var sc = (ShootComponent)newData;
        if(sc.Range != null)
        {
            Range = sc.Range;
        }
    }
}
