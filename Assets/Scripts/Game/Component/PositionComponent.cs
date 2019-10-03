[Component(ComponentType.Position)]
public class PositionComponent : ComponentBase
{
    public Point Position;

    public PositionComponent(Point p, OnMapType type)
    {
        Position = p;
    }

    public override void Update(ComponentBase newData)
    {
        var pc = (PositionComponent)newData;
        if(pc.Position != null)
        {
            Position = pc.Position;
        }
    }
}
