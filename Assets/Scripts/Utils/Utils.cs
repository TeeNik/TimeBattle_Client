using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using UnityEngine;

public static class Utils
{
    public const float MovementSpeed = .75f;

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

    public static T ParseConfig<T>(string fileName)
    {
        var asset = Resources.Load("Configs/" + fileName) as TextAsset;
        return JsonConvert.DeserializeObject<T>(asset.text);
    }

    public static ComponentBase GetComponentFromJson(ComponentType type, string json)
    {
        var tp = ComponentBase.GetClassType(type);
        return (ComponentBase)JsonConvert.DeserializeObject(json, tp);
    }

    public static Color CreateColor(float r, float g, float b, float a = 255)
    {
        return new Color(r / 255.0f, g / 255.0f, b / 255.0f, a / 255);
    }

    public static void PlayWithDelay(Action action, float delay)
    {
        GameLayer.I.StartCoroutine(DelayCoroutine(action, delay));
    }

    private static IEnumerator DelayCoroutine(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

}
