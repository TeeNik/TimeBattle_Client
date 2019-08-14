using System.Linq;

public class MovementSystem : SystemBase<MovementComponent>
{
    public override void Update()
    {
        foreach (var component in Components)
        {
            var entity = GameLayer.I.EntityManager.GetEntity(component.Key);
            var data = component.Value;
            var map = GameLayer.I.MapController;

            if (data.IsInitial)
            {
                var pos = map.SetToPosition(MapData.Player, data.Positions.First());
                entity.transform.position = pos;
            }
        }
    }
}
