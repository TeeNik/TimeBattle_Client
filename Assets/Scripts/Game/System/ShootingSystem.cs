using System.Collections.Generic;
using System.Linq;

public class ShootingSystem : ISystem
{
    private Dictionary<int, ShootComponent> _components = new Dictionary<int, ShootComponent>();
    private readonly Dictionary<int, CharacterView> _views = new Dictionary<int, CharacterView>();

    private readonly List<int> _toDelete = new List<int>();

    public void Update()
    {
        var map = Game.I.MapController;
        var msgs = new List<TakeDamageMsg>();
        var predictionMap = Game.I.UserInputController.ActionController.PredictionMap;
        predictionMap.ClearLayer(Layers.Shooting);

        foreach (var pair in _components)
        {
            if (_toDelete.Contains(pair.Key))
            {
                continue;
            }

            var component = pair.Value;
            var range = pair.Value.Range;

            if (range != null && component.Time > 0)
            {
                _views[pair.Key].PlayStandAnimation(true);
                predictionMap.DrawShootingRange(range);
                var entity = Game.I.EntityManager.GetEntity(pair.Key);
                var info = entity.GetEcsComponent<OperativeInfoComponent>();
                var enemy = Utils.PlayerTypeToMap(Utils.GetOppositePlayer(info.Owner));
                --component.Time;
                foreach (var point in range)
                {
                    var hasEnemy = map.HasEnemy(point, enemy);

                    if (hasEnemy)
                    {
                        var target = map.CheckCover(range, point);
                        if(target.HasValue)
                        {
                            _views[pair.Key].PlayShootAnimation();
                            msgs.Add(new TakeDamageMsg(target.Value, 1));
                        }

                        range.Clear();
                        break;
                    }
                }
            }
            _views[pair.Key].PlayStandAnimation(false);
        }

        foreach (var msg in msgs)
        {
            MakeShoot(msg);
        }

        foreach (var id in _toDelete)
        {
            _components.Remove(id);
            _views.Remove(id);
        }
        _toDelete.Clear();
    }

    private void MakeShoot(TakeDamageMsg msg)
    {
        var target = Game.I.EntityManager.GetEntity(msg.EntityId);
        if (target != null)
        {
            var mc = target.GetEcsComponent<MovementComponent>();
            if (mc.IsMoving)
            {
                mc.OnEndMoving += () => Game.I.Messages.SendEvent(msg);
            }
            else
            {
                Game.I.Messages.SendEvent(msg);
            }
        }
    }

    public void AddComponent(Entity entity, ComponentBase component)
    {
        _components.Add(entity.Id, (ShootComponent)component);
        _views.Add(entity.Id, entity.GetComponent<CharacterView>());
    }

    public void OnUpdateEnd()
    {
        foreach(var comp in _components.Values.Where(c=>c.Range != null && c.Range.Count > 0))
        {
            comp.Range.Clear();
        }
        Game.I.UserInputController.ActionController.PredictionMap.ClearLayer(Layers.Shooting);
    }

    public bool IsProcessing()
    {
        return false;
    }

    public void RemoveComponent(int entityId)
    {
        _toDelete.Add(entityId);
    }
}
