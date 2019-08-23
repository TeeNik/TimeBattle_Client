public enum OperativeType
{
    Soldier
}

public class SpawnEntityDto
{
    public int entityId;
    public OperativeType operativeType;
    public MovementComponent spawnPosition;
}