﻿using DG.Tweening;
using System.Linq;

public class MovementSystem : SystemBase<MovementComponent>
{
    public override void UpdateImpl()
    {
        foreach (var component in Components)
        {
            var entity = Game.I.EntityManager.GetEntity(component.Key);
            var data = component.Value;
            var map = Game.I.MapController;

            var nextPosition = data.Positions.First();
            data.Positions.Remove(nextPosition);

            var pos = map.SetToPosition(MapData.Player, nextPosition);

            if (data.IsInitial)
            {
                entity.transform.position = pos;
            }
            else
            {
                entity.transform.DOMove(pos, 3);// position = pos; 
            }

            if (data.Positions.Count == 0)
            {
                ToDelete.Add(component.Key);
            }
        }
    }

    public override int GetPhaseLength()
    {
        return Components.Max(c=>c.Value.Positions.Count);
    }
}
