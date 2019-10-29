using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ResourceManager : MonoBehaviour
{

    public static ResourceManager Instance;

    public TileBase[] TileBases;
    public TileBase[] FloorTile;
    public TileBase[] FlagArea;
    public Sprite[] Sprites;
    public Entity[] Entities;
    public GameObject Outline;
    public GameObject Flag;
    public GameObject[] CharacterModels;

    public CharacterPrediction CharacterPrediction;

    public void Start()
    {
        Instance = this;
    }

    private static T Get<T>(string name, T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].ToString().Split(' ')[0] == name)
            {
                return array[i];
            }
        }
        return default(T);
    }

    public Entity GetEntity(string name)
    {
        return Get(name, Entities);
    }

    public GameObject GetCharacterModel(string name)
    {
        return Get(name, CharacterModels);
    }

    public Sprite GetSprite(string sprite)
    {
        return Get(sprite, Sprites);
    }
}
