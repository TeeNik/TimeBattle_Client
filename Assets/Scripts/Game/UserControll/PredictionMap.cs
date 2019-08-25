using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PredictionMap : MonoBehaviour
{
    [SerializeField]
    private Tilemap _predictionTilemap;

    [SerializeField]
    private List<GameObject> _objectsPrediction;

    [SerializeField] private Transform _parent;

    public void DrawCharacter()
    {
        var map = Game.I.MapController;
        var position = map.GetTilePositionByMouse();
        var ch = Instantiate(ResourceManager.Instance.CharacterPrediction, _parent);
        ch.transform.position = position;
        _objectsPrediction.Add(ch);
    }

    public void DrawPath(List<Point> path)
    {
        ClearTiles();
        TileBase tile = ResourceManager.Instance.TileBases[(int)TileType.Path];
        foreach (var point in path)
        {
            _predictionTilemap.SetTile(new Vector3Int(point.X, point.Y, 0), tile);
        }
    }

    public void ClearPrediction()
    {
        ClearTiles();
        foreach (var o in _objectsPrediction)
        {
            Destroy(o.gameObject);
        }
        _objectsPrediction.Clear();
    }

    public void ClearTiles()
    {
        _predictionTilemap.ClearAllTiles();
    }

}
