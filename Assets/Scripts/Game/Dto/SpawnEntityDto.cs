﻿using System.Collections.Generic;

public enum OperativeType
{
    Assault,
    Ranger,
    Sniper,
}

public class SpawnEntityDto
{
    public string PrefabName;
    public List<ComponentDto> InitialComponents = new List<ComponentDto>();
}