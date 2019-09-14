using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Object;

public class EntityController
{

    private readonly Dictionary<int, Entity> _entities;
    private static int _idCounter = 0;

    public EntityController()
    {
        _entities = new Dictionary<int, Entity>();
    }

    public Entity GetEntity(int id)
    {
        return _entities[id];
    }

    public void DestroyEntity(int entityId)
    {
        var entity = _entities[entityId];
        entity.ClearComponents();
        _entities.Remove(entityId);
        GameObject.Destroy(entity.gameObject);
    }

    public void CreateEntity(SpawnEntityDto dto)
    {
        var playerPrefab = ResourceManager.Instance.GetEntity(dto.PrefabName);
        var entity = Instantiate(playerPrefab);
        _entities.Add(_idCounter, entity);
        entity.Init(_idCounter);
        foreach (var comp in dto.InitialComponents)
        {
            entity.AddComponent(comp);
        }
        ++_idCounter;
    }
}
