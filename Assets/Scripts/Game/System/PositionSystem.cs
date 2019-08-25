public class PositionSystem : SystemBase<PositionComponent>
{

    public override void OnComponentAdded(int entityId, PositionComponent component)
    {
        base.OnComponentAdded(entityId, component);
        var entity = Game.I.EntityManager.GetEntity(entityId);
        var pos = Game.I.MapController.SetToPosition(MapData.Player, component.Position);
        entity.transform.position = pos;
    }

    public override void UpdateImpl()
    {

    }
}