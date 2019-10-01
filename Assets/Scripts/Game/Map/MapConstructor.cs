﻿using UnityEngine;
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
    public MapData[][] GenerateMap(Tilemap tilemap)
    {
        var map = GameLayer.I.GameBalance.Map;

        MapData[][] mapData = new MapData[map.Length][];
        TileBase[] bases = ResourceManager.Instance.TileBases;
        TileBase[] floor = ResourceManager.Instance.FloorTile;

        for (int i = 0; i < map.Length; i++)
        {
            mapData[i] = new MapData[map[i].Length];
            for (int j = 0; j < map[i].Length; j++)
            {
                //TODO refactor
                var isWall = (OnMapType) map[i][j] == OnMapType.Wall;
                var tileType = isWall ? TileType.Wall : TileType.Floor;
                mapData[i][j].Type = isWall ? OnMapType.Wall : OnMapType.Empty;

                if (isWall)
                {
                    tilemap.SetTile(new Vector3Int(i, j, 0), bases[(int)tileType]);
                }
                else
                {
                    var num = Random.Range(0, 10);
                    var tile = floor[num >= floor.Length ? 3 : num];
                    tilemap.SetTile(new Vector3Int(i, j, 0), tile);
                }
            }
        }

        return mapData;
    }
}
