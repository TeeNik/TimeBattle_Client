using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PredictionMap : MonoBehaviour
{

    private enum Layers
    {
        Base = 0,
        Shooting = 1,
    }

    [SerializeField] private List<Tilemap> _layers;

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

    public void DrawShootingRange(List<Point> range)
    {
        TileBase tile = ResourceManager.Instance.TileBases[(int)TileType.Shoot];
        foreach (var point in range)
        {
            _layers[(int)Layers.Shooting].SetTile(new Vector3Int(point.X, point.Y, 0), tile);
        }
    }

    public void ClearShootingLayer()
    {
        _layers[(int)Layers.Shooting].ClearAllTiles();
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
