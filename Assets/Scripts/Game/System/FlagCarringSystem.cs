using System.Collections.Generic;
using UnityEngine;

public class FlagCarringSystem : ISystem
{
    private readonly Dictionary<int, FlagCarryComponent> _components = new Dictionary<int, FlagCarryComponent>();
    private readonly EventListener _eventListener = new EventListener();
    private Point _flagPosition;
    private GameObject _flag;

    private FlagQuestConfig _flagConfig;

    public FlagCarringSystem()
    {
        _eventListener.Add(Game.I.Messages.Subscribe<TakeFlagMsg>(OnTakeFlagMsg));

        _flagConfig = Utils.ParseConfig<FlagQuestConfig>("flag_area");
        _flagPosition = _flagConfig.FlagSpawn;
        PlaceFlag(_flagPosition);


        foreach (var areaData in _flagConfig.AreaData)
        {
            foreach (var point in areaData.Area)
            {
                var tile = Game.I.MapController.OutlinePool.GetFromPool();
                tile.transform.position = Game.I.MapController.GetTileWorldPosition(point);
            }
        }
    }

    public void PlaceFlag(Point point)
    {
        var flagBase = ResourceManager.Instance.Flag;
        var pos = Game.I.MapController.GetTileWorldPosition(point);
        _flag = GameObject.Instantiate(flagBase);
        _flag.transform.position = pos;
    }

    private void OnTakeFlagMsg(TakeFlagMsg msg)
    {
        var entity = Game.I.EntityManager.GetEntity(msg.EntityId);
        _flag.transform.SetParent(entity.transform);
        entity.AddComponent(new FlagCarryComponent());
    }

    public void Update()
    {
        foreach (var component in _components)
        {
            var entity = Game.I.EntityManager.GetEntity(component.Key);
            var mc = entity.GetEcsComponent<MovementComponent>();
            _flagPosition = mc.Position;

            var playerType = entity.GetEcsComponent<OperativeInfoComponent>().Owner;
            var area = _flagConfig.AreaData.Find(a => a.Player == playerType);
            foreach (var point in area.Area)
            {
                if (point.Equals(_flagPosition))
                {
                    Debug.Log($"{playerType} wins!");
                }
            }
        }

        if (_components.Count == 0)
        {
            var mapData = Game.I.MapController.MapDatas[_flagPosition.X][_flagPosition.Y];
            if (mapData.EntityId != null)
            {
                var entity = Game.I.EntityManager.GetEntity(mapData.EntityId.Value);
                var mc = entity.GetEcsComponent<MovementComponent>();
                mc.OnEndMoving += () =>
                {
                    if (!entity.IsDestroyed)
                    {
                        _flag.transform.SetParent(entity.transform);
                        _flag.transform.localPosition = new Vector3(-1.37f, 0.33f);
                        entity.AddComponent(new FlagCarryComponent());
                    }
                };
            }
        }
    }

    public void AddComponent(Entity entity, ComponentBase component)
    {
        _components.Add(entity.Id, (FlagCarryComponent)component);
    }

    public void RemoveComponent(int entityId)
    {
        _components.Remove(entityId);
        PlaceFlag(_flagPosition);
    }

    public bool IsProcessing()
    {
        return false;
    }

    public int GetPhaseLegth()
    {
        return 1;
    }
}
