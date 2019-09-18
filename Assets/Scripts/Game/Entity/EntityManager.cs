using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Object;

public class EntityController
{

    private readonly Dictionary<int, Entity> _entities;
    private static int _idCounter = 0;
    private readonly List<int> _toDelete = new List<int>();

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
        _toDelete.Add(entityId);
        var entity = _entities[entityId];
        entity.ClearComponents();
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

    public void Update()
    {
        foreach (var id in _toDelete)
        {
            var entity = _entities[id];
            _entities.Remove(id);
            GameObject.Destroy(entity.gameObject);
        }
        _toDelete.Clear();
    }
}
