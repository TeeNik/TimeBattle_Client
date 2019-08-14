using UnityEngine;
using UnityEngine.Tilemaps;

public class ResourceManager : MonoBehaviour
{

    public static ResourceManager Instance;

    public TileBase[] TileBases;
    public Character CharacterPrefab;

    public void Start()
    {
        Instance = this;
    }

}
