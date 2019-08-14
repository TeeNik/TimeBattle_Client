using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{

    public Tilemap tileMap;
    public Transform Pointer;
    public Grid grid;

    private MapData[][] _mapDatas;
    private bool _isInited;


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
            Pointer.position = tileMap.GetCellCenterWorld(new Vector3Int(tile.x, tile.y, tile.z));
        }
        //Debug.Log($"Pos: x={tile.x} y={tile.y}");
    }

    public Vector3 SetToPosition(MapData objectToMove, Vector3Int position)
    {
        var worldPos = tileMap.GetCellCenterWorld(position);
        _mapDatas[position.x][position.y] = objectToMove;
        return worldPos;
    }
}
