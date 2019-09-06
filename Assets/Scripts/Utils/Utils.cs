using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class Utils
{
    public static IEnumerable<Type> GetTypesOfParent(Type t)
    {
        return Assembly.GetAssembly(t).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && t.IsAssignableFrom(myType));
    }

    public static OnMapType PlayerTypeToMap(PlayerType player)
    {
        return player == PlayerType.Player1 ? OnMapType.Player1 : OnMapType.Player2;
    }

    public static PlayerType GetOppositePlayer(PlayerType type)
    {
        return type == PlayerType.Player1 ? PlayerType.Player2 : PlayerType.Player1;
    }

    public static Type ActionTypeToComponent(ActionType type)
    {
        switch (type)
        {
            case ActionType.Move:
                return typeof(MovementComponent);
            case ActionType.Shoot:
                return typeof(ShootComponent);
        }
        throw new ArgumentException();
    }

    public static List<T> ParseConfig<T>(string fileName)
    {
        var asset = Resources.Load(fileName) as TextAsset;
        return JsonUtility.FromJson<List<T>>(asset.text);
    }

}
