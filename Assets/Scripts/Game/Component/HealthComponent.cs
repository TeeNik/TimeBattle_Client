public class HealthComponent : ComponentBase
{

    public int Health;

    public HealthComponent(int health)
    {
        Health = health;
    }

    public void Update(ComponentBase newData)
    {
        var hc = (HealthComponent)newData;
        Health = hc.Health;
    }
}
