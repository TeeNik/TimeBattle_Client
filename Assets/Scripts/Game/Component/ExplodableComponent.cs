[Component(ComponentType.Explodable)]
public class ExplodableComponent : ComponentBase
{

    public Point Target;
    public bool IsExloding;

    public override void Update(ComponentBase newData)
    {
        var gt = (ExplodableComponent)newData;
        Target = gt.Target;
    }
}
