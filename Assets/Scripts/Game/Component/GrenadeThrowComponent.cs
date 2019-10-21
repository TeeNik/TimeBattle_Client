using System.Collections.Generic;

[Component(ComponentType.GrenadeThrow)]
public class GrenadeThrowComponent : ComponentBase
{
    public Point Target;
    public List<Point> Range;

    public GrenadeThrowComponent()
    {

    }

    public GrenadeThrowComponent(Point target, List<Point> range)
    {
        Target = target;
        Range = range;
    }

    public override void Update(ComponentBase newData)
    {
        var gt = (GrenadeThrowComponent) newData;
        Target = gt.Target;
        Range = gt.Range;
    }

    public override int GetUpdateLength()
    {
        return 5;
    }
}
