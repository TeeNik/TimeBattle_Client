using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileType
{
    Wall,
    Floor,
    Path,
    Shoot,
}

public class MapConstructor
{
    private readonly List<Tilemap> _layers;

    public MapConstructor(List<Tilemap> layers)
    {
        _layers = layers;
    }

    public MapData[][] GenerateMap()
    {
        var config = GameLayer.I.GameBalance.MapConfig;
        var map = config.map;
        var decor = config.decorationMap;

        MapData[][] mapData = new MapData[map.Length][];
        TileBase[] floor = ResourceManager.Instance.FloorTile;
        TileBase[] decarationTiles = ResourceManager.Instance.DecarationTiles;

        for (int i = 0; i < map.Length; i++)
        {
            mapData[i] = new MapData[map[i].Length];
            for (int j = 0; j < map[i].Length; j++)
            {
                //TODO refactor
                var isWall = (OnMapType) map[i][j] == OnMapType.Wall;
                mapData[i][j].Type = isWall ? OnMapType.Wall : OnMapType.Empty;

                if (isWall)
                {
                    var tile = decarationTiles[decor[i][j] - 1];
                    _layers[0].SetTile(new Vector3Int(i, j, 0), tile);
                }
                else
                {
                    var num = Random.Range(0, 10);
                    var tile = floor[num >= floor.Length ? 3 : num];
                    _layers[0].SetTile(new Vector3Int(i, j, 0), tile);
                }
            }
        }

        return mapData;
    }
}
