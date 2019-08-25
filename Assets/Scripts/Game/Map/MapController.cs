using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{

    public Tilemap tileMap;
    public Transform Pointer;
    public Grid grid;
    
    public PathFinder PathFinder { get; private set; }

    private MapData[][] _mapDatas;
    private bool _isInited;
    private readonly Vector3 HidePos = new Vector3(1000, 1000);


    public void Init()
    {
        MapContructor contructor = new MapContructor();
        _mapDatas = contructor.GenerateMap(tileMap);
        PathFinder = new PathFinder(_mapDatas);
        _isInited = true;
    }

    void Update()
    {
        if (_isInited)
        {
            var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var tile = grid.WorldToCell(mouse);

            var i = tile.x;
            var j = tile.y;
            bool isShow = IsInBounds(tile)&& _mapDatas[i][j] != MapData.Wall;
            Pointer.position = isShow ? tileMap.GetCellCenterWorld(new Vector3Int(tile.x, tile.y, tile.z)) : HidePos;
        }
    }

    public Vector3 SetToPosition(MapData objectToMove, Point position)
    {
        var tile = new Vector3Int(position.X, position.Y, 0);
        var worldPos = tileMap.GetCellCenterWorld(tile);
        _mapDatas[position.X][position.Y] = objectToMove;
        return worldPos;
    }

    public Vector3 MoveToPosition(MapData objectToMove, Point from, Point to)
    {
        var tile = new Vector3Int(to.X, to.Y, 0);
        var worldPos = tileMap.GetCellCenterWorld(tile);
        _mapDatas[from.X][from.Y] = MapData.Empty;
        _mapDatas[to.X][to.Y] = objectToMove;
        return worldPos;
    }

    public Vector3 GetTilePositionByMouse()
    {
        var tile = GetTileByMouse();
        return tileMap.GetCellCenterWorld(new Vector3Int(tile.x, tile.y, tile.z));
    }

    public Vector3Int GetTileByMouse()
    {
        var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouse);
    }

    public Vector3Int GetTileByVector3(Vector3 pos)
    {
        return grid.WorldToCell(pos);
    }

    public bool IsWalkable(Vector3Int tile)
    {
        return IsInBounds(tile) && _mapDatas[tile.x][tile.y] == MapData.Empty;
    }

    private bool IsInBounds(Vector3Int tile)
    {
        var i = tile.x;
        var j = tile.y;
        return i > 0 && i < _mapDatas.Length && j > 0 && j < _mapDatas[0].Length;
    }

}
