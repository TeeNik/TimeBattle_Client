using UnityEngine;
using UnityEngine.Tilemaps;

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
        new[] {0, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 0, 0, 0, 1, 0},
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

                if (value == MapData.Wall)
                {
                    tilemap.SetTile(new Vector3Int(i, j, 0), bases[0]);
                }
                else
                {
                    tilemap.SetTile(new Vector3Int(i, j, 0), bases[1]);
                }
            }
        }

        return mapData;
    }
}
