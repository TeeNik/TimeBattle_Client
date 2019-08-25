public class PositionSystem : SystemBase<PositionComponent>
{

    public override void OnComponentAdded(int entityId, PositionComponent component)
    {
        base.OnComponentAdded(entityId, component);
        var entity = Game.I.EntityManager.GetEntity(entityId);
        var info = Game.I.SystemController.OperativeInfoSystem.GetComponent(entity.Id);
        var mapData = info.Owner == PlayerType.Player1 ? MapData.Player1 : MapData.Player2;
        var pos = Game.I.MapController.SetToPosition(mapData, component.Position);
        entity.transform.position = pos;
    }

    public override void UpdateImpl()
    {

    }
}