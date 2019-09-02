using System.Collections.Generic;

public enum OperativeType
{
    Soldier
}

public class SpawnEntityDto
{
    public int entityId;
    public List<ComponentBase> InitialComponents = new List<ComponentBase>();

    //public OperativeInfoCmponent operativeInfo;
    //public MovementComponent spawnPosition;
}