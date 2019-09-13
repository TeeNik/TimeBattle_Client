using System.Collections.Generic;

public class ComponentDto
{
    public ComponentType Type;
    public List<string> Components;
}

public class ActionPhase
{
    public int entityId;
    public List<ComponentBase> phases;
}
