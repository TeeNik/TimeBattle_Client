using System.Linq;

public class MovementSystem : SystemBase<MovementComponent>
{
    public override void Update()
    {
        foreach (var component in Components)
        {
            var entity = RoomModel.I.EntityManager.GetEntity(component.Key);
            var data = component.Value;
            var map = RoomModel.I.MapController;

            if (data.IsInitial)
            {
                var pos = map.SetToPosition(MapData.Player, data.Positions.First());
                entity.transform.position = pos;
            }
        }
    }
}
