using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{

    public Tilemap tileMap;
    public Transform Pointer;
    public Grid grid;
    
    public PathFinder PathFinder { get; private set; }
    public OutlinePool OutlinePool { get; private set; }

    public MapData[][] MapDatas { get; private set; }
    private bool _isInited;
    private readonly Vector3 HidePos = new Vector3(1000, 1000);


    public void Init()
    {
        MapConstructor constructor = new MapConstructor();
        MapDatas = constructor.GenerateMap(tileMap);
        PathFinder = new PathFinder(MapDatas);
        OutlinePool = new OutlinePool(transform);
        _isInited = true;
    }

    //TODO Remove to UI
    void Update()
    {
        if (_isInited)
        {
            var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var tile = grid.WorldToCell(mouse);

            var i = tile.x;
            var j = tile.y;
            bool isShow = IsInBounds(tile)&& MapDatas[i][j].Type != OnMapType.Wall;
            Pointer.position = isShow ? tileMap.GetCellCenterWorld(new Vector3Int(tile.x, tile.y, tile.z)) : HidePos;
        }
    }

    public void SetMapData(Point point, OnMapType type)
    {
        MapDatas[point.X][point.Y].Type = type;
        MapDatas[point.X][point.Y].EntityId = null;
    }

    public Vector3 SetToPosition(int id, OnMapType objectToMove, Point position)
    {
        var tile = new Vector3Int(position.X, position.Y, 0);
        var worldPos = tileMap.GetCellCenterWorld(tile);
        MapDatas[position.X][position.Y].Type = objectToMove;
        MapDatas[position.X][position.Y].EntityId = id;
        return worldPos;
    }

    public Vector3 MoveToPosition(int id, OnMapType objectToMove, Point from, Point to)
    {
        var tile = new Vector3Int(to.X, to.Y, 0);
        var worldPos = tileMap.GetCellCenterWorld(tile);
        MapDatas[from.X][from.Y].Type = OnMapType.Empty;
        MapDatas[from.X][from.Y].EntityId = null;
        MapDatas[to.X][to.Y].Type = objectToMove;
        MapDatas[to.X][to.Y].EntityId = id;
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

    public Vector3 GetTileWorldPosition(Point point)
    {
        return tileMap.GetCellCenterWorld(new Vector3Int(point.X, point.Y, 0));
    }

    public Vector3Int GetTileByVector3(Vector3 pos)
    {
        return grid.WorldToCell(pos);
    }

    public bool IsWalkable(Vector3Int tile)
    {
        return IsInBounds(tile) && MapDatas[tile.x][tile.y].Type == OnMapType.Empty;
    }

    private bool IsInBounds(Vector3Int tile)
    {
        var i = tile.x;
        var j = tile.y;
        return i > 0 && i < MapDatas.Length && j > 0 && j < MapDatas[0].Length;
    }

    private bool IsInBounds(Point p)
    {
        var i = p.X;
        var j = p.Y;
        return i > 0 && i < MapDatas.Length && j > 0 && j < MapDatas[0].Length;
    }

    public bool HasEnemy(Point p, OnMapType enemy)
    {
        return IsInBounds(p) && MapDatas[p.X][p.Y].Type == enemy;
    }

    //TODO refactor 
    public int? CheckCover(List<Point> range, Point target)
    {
        for (var i = 0; i < range.Count; i++)
        {
            var p = range[i];
            var current = MapDatas[p.X][p.Y].Type;
            if (target.Equals(p))
            {
                if (i > 0)
                {
                    var prev = range[i - 1];
                    if (MapDatas[prev.X][prev.Y].Type == OnMapType.Cover)
                    {
                        return GetEntityByPoint(prev);
                    }
                }
                return GetEntityByPoint(p);
            }
            if (current == OnMapType.Wall)
            {
                return null;
            }
        }

        return null;
    }

    public int? GetEntityByPoint(Point p)
    {
        return MapDatas[p.X][p.Y].EntityId;
    }
}
