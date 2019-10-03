using System.Collections.Generic;

public enum OperativeType
{
    Assault,
    Ranger,
    Sniper,
}

public class SpawnEntityDto
{
    public int Id;
    public string PrefabName;
    public List<ComponentBase> InitialComponents = new List<ComponentBase>();
}