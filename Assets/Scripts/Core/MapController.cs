using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{

    public Tilemap tileMap;
    public Transform Pointer;
    public Grid grid;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var tile = grid.WorldToCell(mouse);
        Pointer.position = tileMap.GetCellCenterWorld(new Vector3Int(tile.x, tile.y, tile.z));
        Debug.Log($"Pos: x={tile.x} y={tile.y}");
    }
}
