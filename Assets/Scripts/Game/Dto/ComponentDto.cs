using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class ActionPhase
{
    public int entityId;
    public List<ComponentDto> phases;
}

public class ComponentDto
{
    public ComponentType Type;
    public string Component;

    public ComponentDto()
    {

    }

    public ComponentDto(ComponentBase component)
    {
        Type = ComponentBase.GetComponentType(component.GetType());
        Component = JsonUtility.ToJson(component);
    }

    public ComponentBase ToComponentBase()
    {
        var type = ComponentBase.GetClassType(Type);
        return (ComponentBase)JsonConvert.DeserializeObject(Component, type);
    }
}
