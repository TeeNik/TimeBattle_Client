using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

public enum ComponentType
{
    Movement,
    Shoot,
    OperativeInfo,
    Health,
    Position,
    CharacterAction,
    FlagCarry,
}

public class ComponentAttribute : Attribute
{
    public ComponentType Type;

    public ComponentAttribute(ComponentType type)
    {
        Type = type;
    }
}

public abstract class ComponentBase
{
    private static Dictionary<ComponentType, Type> _types;

    public static Type GetComponentType(ComponentType type)
    {
        if (_types == null)
        {
            _types = new Dictionary<ComponentType, Type>();
            var baseType = typeof(ComponentBase);
            var list = Utils.GetTypesOfParent(baseType);
            foreach (var child in list)
            {

                var attr = child.GetCustomAttributes(typeof(ComponentAttribute), false);
                Assert.IsFalse(attr.Length == 0, $"Every component should have ComponentAttribute. {child} does not have.");
                if (attr.Length > 0)
                {
                    _types.Add(((ComponentAttribute)(attr[0])).Type, child);
                }
            }
        }
        return _types[type];
    }

    public abstract void Update(ComponentBase newData);

}
