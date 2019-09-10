using UnityEngine;
using UnityEngine.Tilemaps;

public class ResourceManager : MonoBehaviour
{

    public static ResourceManager Instance;

    public TileBase[] TileBases;

    public Entity[] Entities;

    public CharacterPrediction CharacterPrediction;

    public void Start()
    {
        Instance = this;
    }

    public static Entity GetEntity(string name)
    {
        return Get($"{name} (Character)", Instance.Entities);
    }

    private static T Get<T>(string name, T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].ToString() == name)
            {
                return array[i];
            }
        }
        return default(T);
    }

}
