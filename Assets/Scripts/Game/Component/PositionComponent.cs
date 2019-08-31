public class PositionComponent : ComponentBase
{
    public Point Position;

    public PositionComponent(Point p)
    {
        Position = p;
    }

    public void Update(ComponentBase newData)
    {
        var pc = (PositionComponent)newData;
        if(pc.Position != null)
        {
            Position = pc.Position;
        }
    }
}
