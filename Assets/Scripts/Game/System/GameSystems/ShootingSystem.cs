using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : ISystem
{
    public void Update()
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

    public void AddComponent(ComponentBase component)
    {
        
    }

    public bool IsProcessing()
    {
        return false;
    }
}
