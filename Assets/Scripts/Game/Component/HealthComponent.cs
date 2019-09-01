public class HealthComponent : ComponentBase
{

    public int CurrentHealth;
    public readonly int MaxHealth;


    public HealthComponent(int maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
    }

    public void Update(ComponentBase newData)
    {
        var hc = (HealthComponent)newData;
        CurrentHealth = hc.CurrentHealth;
    }
}
