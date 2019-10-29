using System.Collections.Generic;

[Component(ComponentType.Explodable)]
public class ExplodableComponent : ComponentBase
{

    public readonly Point Target;
    public readonly Point StartPosition;
    public readonly List<Point> Range;
    public bool IsExloding;

    public ExplodableComponent(Point startPosition, Point target, List<Point> range)
    {
        Target = target;
        StartPosition = startPosition;
        Range = range;
    }

    public override void Update(ComponentBase newData)
    {
    }
}
