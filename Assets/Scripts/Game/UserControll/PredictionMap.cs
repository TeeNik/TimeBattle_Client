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
        var map = RoomModel.I.MapController;
        var position = map.GetTilePositionByMouse();
        var ch = Instantiate(ResourceManager.Instance.CharacterPrediction, _parent);
        ch.transform.position = position;
    }

    public void DrawPath()
    {

    }

    public void ClearPrediction()
    {
        foreach (var o in _objectsPrediction)
        {
            _objectsPrediction.Remove(o);
            Destroy(o);
        }
    }

    private void ClearTiles()
    {
        _predictionTilemap.ClearAllTiles();
    }

}
