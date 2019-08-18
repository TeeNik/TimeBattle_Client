﻿using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{

    public Tilemap tileMap;
    public Transform Pointer;
    public Grid grid;

    private MapData[][] _mapDatas;
    private bool _isInited;
    private readonly Vector3 HidePos = new Vector3(1000, 1000);


    public void Init()
    {
        MapContructor contructor = new MapContructor();
        _mapDatas = contructor.GenerateMap(tileMap);
        _isInited = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isInited)
        {
            var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var tile = grid.WorldToCell(mouse);

            var i = tile.x;
            var j = tile.y;
            bool isShow = false;
            if(i > 0 && i < _mapDatas.Length && j > 0 && j < _mapDatas[0].Length && _mapDatas[i][j] != MapData.Wall)
            {
                isShow = true;
            }
            Pointer.position = isShow ? tileMap.GetCellCenterWorld(new Vector3Int(tile.x, tile.y, tile.z)) : HidePos;
        }
    }

    public Vector3 SetToPosition(MapData objectToMove, Vector3Int position)
    {
        var worldPos = tileMap.GetCellCenterWorld(position);
        _mapDatas[position.x][position.y] = objectToMove;
        return worldPos;
    }

}
