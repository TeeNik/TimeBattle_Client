using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PredictionMap : MonoBehaviour
{
    [SerializeField]
    private Tilemap _predictionTilemap;

    [SerializeField]
    private List<CharacterPrediction> _objectsPrediction;

    [SerializeField] private Transform _parent;

    public void DrawCharacter(Character reference, Point point)
    {
        var map = Game.I.MapController;
        var position = map.GetTileWorldPosition(point);
        var ch = Instantiate(ResourceManager.Instance.CharacterPrediction, _parent);
        ch.transform.position = reference.transform.position;
        reference.transform.position = position;
        ch.Init(reference);
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
        _objectsPrediction.Reverse();
        foreach (var o in _objectsPrediction)
        {
            o.MoveReferance();
            Destroy(o.gameObject);
        }
        _objectsPrediction.Clear();
    }

    public void ClearTiles()
    {
        _predictionTilemap.ClearAllTiles();
    }

}
