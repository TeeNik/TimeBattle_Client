using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class Utils
{
    public static IEnumerable<Type> GetTypesOfParent(Type t)
    {
        return Assembly.GetAssembly(t).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && t.IsAssignableFrom(myType));
    }
}
