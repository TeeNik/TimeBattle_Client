using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum Layers
{
    Temporary = 0,
    Shooting = 1,
    Movement = 2,
}

public class PredictionMap : MonoBehaviour
{
    [SerializeField] private List<Tilemap> _layers;

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

    public void DrawMoveInput(List<Point> path)
    {
        ClearLayer(Layers.Temporary);
        TileBase tile = ResourceManager.Instance.TileBases[(int)TileType.Path];
        DrawTileOnLayer(path, Layers.Temporary, tile);
    }

    public void DrawMovePath(List<Point> path)
    {
        TileBase tile = ResourceManager.Instance.TileBases[(int)TileType.Path];
        DrawTileOnLayer(path, Layers.Movement, tile);
    }

    public void DrawShootingRange(List<Point> range)
    {
        TileBase tile = ResourceManager.Instance.TileBases[(int)TileType.Shoot];
        DrawTileOnLayer(range, Layers.Shooting, tile);
    }

    public void DrawShootInput(List<Point> range)
    {
        TileBase tile = ResourceManager.Instance.TileBases[(int)TileType.Shoot];
        DrawTileOnLayer(range, Layers.Temporary, tile);
    }

    private void DrawTileOnLayer(List<Point> area, Layers layer, TileBase tile)
    {
        foreach (var point in area)
        {
            _layers[(int)layer].SetTile(new Vector3Int(point.X, point.Y, 0), tile);
        }
    }

    public void ClearLayer(Layers layer)
    {
        _layers[(int)layer].ClearAllTiles();
    }

    public void ClearAll()
    {
        foreach (var layer in _layers)
        {
            layer.ClearAllTiles();
        }

        ReverseCharacterPrediction();
    }

    public void ReverseCharacterPrediction()
    {
        ClearLayer(Layers.Temporary);
        _objectsPrediction.Reverse();
        foreach (var o in _objectsPrediction)
        {
            o.MoveReferance();
            Destroy(o.gameObject);
        }
        _objectsPrediction.Clear();
    }
}
