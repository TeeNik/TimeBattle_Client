using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileType
{
    Wall,
    Floor,
    Path
}

public class MapContructor
{
    private const int Width = 17;
    private const int Height = 11;

    private readonly int[][] _map = {
        new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        new[] {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
        new[] {0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0},
        new[] {0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
        new[] {0, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 0},
        new[] {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 1, 0},
        new[] {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 1, 0},
        new[] {0, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0},
        new[] {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 0},
        new[] {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
        new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
    };

    public MapData[][] GenerateMap(Tilemap tilemap)
    {
        MapData[][] mapData = new MapData[Height][];
        TileBase[] bases = ResourceManager.Instance.TileBases;

        for (int i = 0; i < Height; i++)
        {
            mapData[i] = new MapData[Width];
            for (int j = 0; j < Width; j++)
            {
                var value = (MapData) _map[i][j];
                mapData[i][j] = value;
                var tileType = value == MapData.Wall ? TileType.Wall : TileType.Floor;
                tilemap.SetTile(new Vector3Int(i, j, 0), bases[(int)tileType]);
            }
        }

        return mapData;
    }
}
