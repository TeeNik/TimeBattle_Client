using System;
using UnityEngine;

public class FlagController : IDisposable
{
    private EventListener _eventListener = new EventListener();

    public FlagController()
    {
        _eventListener = new EventListener();
        //_eventListener.Add(Game.I.Messages.Subscribe());
        Game.I.SystemController.Systems.Add(typeof(FlagCarryComponent), new FlagCarringSystem());
        DrawFlagsArea();
    }

    private void DrawFlagsArea()
    {
        var flagConfig = GameLayer.I.GameBalance.GetFlagQuestConfig();
        var layer = Game.I.MapController.MapLayers[1];
        var tiles = ResourceManager.Instance.FlagArea;
        foreach (var areaData in flagConfig.AreaData)
        {
            for (var i = 0; i < areaData.Area.Count; i++)
            {
                var point = areaData.Area[i];
                var pos = new Vector3Int(point.X, point.Y, 0);
                layer.SetTile(pos, tiles[i]);
                layer.SetColor(pos, areaData.Player == Game.I.PlayerType ? Color.green : Color.red);
            }
        }
    }

    public void Dispose()
    {
        _eventListener.Clear();
    }
}
