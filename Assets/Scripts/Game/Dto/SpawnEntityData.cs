using System.Collections.Generic;

public enum OperativeType
{
    Assault,
    Ranger,
    Soldier,
    Sniper,
}

public class SpawnEntityData
{
    public string PrefabName;
    public List<ComponentBase> InitialComponents = new List<ComponentBase>();
}