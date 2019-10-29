using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Object;

public class EntityController
{

    private readonly Dictionary<int, Entity> _entities;
    private static int _idCounter = 0;
    private readonly Transform _parent;

    public EntityController()
    {
        _entities = new Dictionary<int, Entity>();
        _parent = Game.I.transform.GetChild(0);
    }

    public Entity GetEntity(int id)
    {
        return _entities.ContainsKey(id) ?_entities[id] : null;
    }

    public void DestroyEntity(int entityId)
    {
        var entity = _entities[entityId];
        entity.Destroy();
        _entities.Remove(entityId);
        //GameObject.Destroy(entity.gameObject);
    }

    public void CreateEntity(SpawnEntityData data)
    {
        var playerPrefab = ResourceManager.Instance.GetEntity(data.PrefabName);
        var entity = Instantiate(playerPrefab, _parent);
        _entities.Add(_idCounter, entity);
        entity.Init(_idCounter);
        foreach (var comp in data.InitialComponents)
        {
            entity.AddComponent(comp);
        }
        ++_idCounter;
    }
}
