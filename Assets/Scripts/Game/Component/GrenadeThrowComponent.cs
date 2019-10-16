[Component(ComponentType.GrenadeThrow)]
public class GrenadeThrowComponent : ComponentBase
{
    public Point Target;

    public GrenadeThrowComponent()
    {

    }

    public GrenadeThrowComponent(Point target)
    {
        Target = target;
    }

    public override void Update(ComponentBase newData)
    {
        var gt = (GrenadeThrowComponent) newData;
        Target = gt.Target;
    }
}
