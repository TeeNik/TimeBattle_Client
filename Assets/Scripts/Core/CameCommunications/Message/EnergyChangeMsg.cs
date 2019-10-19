public class EnergyChangeMsg
{

    public readonly int EntityId;
    public readonly int Energy;

    public EnergyChangeMsg(int entityId, int energy)
    {
        Energy = energy;
        EntityId = entityId;
    }
}
