using UnityEngine;
using UnityEngine.Tilemaps;

public class ResourceManager : MonoBehaviour
{

    public static ResourceManager Instance;

    public TileBase[] TileBases;
    public Sprite[] Sprites;

    public Entity CharacterBase;
    public Entity CoverBase;
    public CharacterPrediction CharacterPrediction;

    public void Start()
    {
        Instance = this;
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

    public Sprite GetSprite(string sprite)
    {
        return Get(sprite, Sprites);
    }
}
