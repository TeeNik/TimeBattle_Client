public class TakeDamageMsg
{

    public readonly int EntityId;
    public readonly int Damage;

    public TakeDamageMsg(int entityId, int damage)
    {
        Damage = damage;
        EntityId = entityId;
    }
}
