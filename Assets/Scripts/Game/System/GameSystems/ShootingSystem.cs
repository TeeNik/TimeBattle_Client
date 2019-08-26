using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : GameSystem<ShootComponent>
{
    public override bool IsProcessing()
    {
        return false;
    }

    public override void UpdateImpl()
    {
        var map = Game.I.MapController;
        var system = Game.I.SystemController;

        foreach (var component in Components)
        {
            var range = component.Value.Range;
            var info = system.OperativeInfoSystem.GetComponent(component.Key);
            var enemy = Utils.PlayerTypeToMap(Utils.GetOppositePlayer(info.Owner));
            foreach(var point in range)
            {
                var hasEnemy = map.HasEnemy(point, enemy);

                if (hasEnemy)
                {
                    Debug.Log("Shoot!!!");
                    break;
                }
            }
        }
    }
}
