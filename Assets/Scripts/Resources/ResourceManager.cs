using UnityEngine;
using UnityEngine.Tilemaps;

public class ResourceManager : MonoBehaviour
{

    public static ResourceManager Instance;

    public TileBase[] TileBases;

    public Character[] CharacterPrefabs;

    public CharacterPrediction CharacterPrediction;

    public void Start()
    {
        Instance = this;

        GetCharacter(PlayerType.Player1, OperativeType.Soldier);
    }

    public static Character GetCharacter(PlayerType player, OperativeType operative)
    {
        var name = $"{operative}_{player}";
        return Get($"{name} (Character)", Instance.CharacterPrefabs);
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
