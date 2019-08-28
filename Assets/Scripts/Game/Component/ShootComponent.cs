using System.Collections.Generic;

public class ShootComponent : ComponentBase
{
    public List<Point> Range;

    public ShootComponent(List<Point> range)
    {
        Range = range;
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
