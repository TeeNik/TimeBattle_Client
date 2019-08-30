public class HealthComponent : ComponentBase
{

    public int Health;

    public override void UpdateComponent(ComponentBase newData)
    {
        var hc = (HealthComponent) newData;
        Health = hc.Health;
    }
}
