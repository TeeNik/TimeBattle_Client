[Component(ComponentType.Explodable)]
public class ExplodableComponent : ComponentBase
{

    public readonly Point Target;
    public readonly Point StartPosition;
    public bool IsExloding;

    public ExplodableComponent(Point startPosition, Point target)
    {
        Target = target;
        StartPosition = startPosition;
    }

    public override void Update(ComponentBase newData)
    {
    }
}
