using System.Collections.Generic;
using UnityEngine;

public class EntityManager
{

    private readonly Dictionary<int, Entity> _entities;
    private static int _idCounter = 0;

    public EntityManager()
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

    public void CreatePlayer(SpawnEntityDto dto)
    {
        var info = (OperativeInfoCmponent)dto.InitialComponents.Find(c => c.GetType() == typeof(OperativeInfoCmponent));
        var playerPrefab = ResourceManager.GetCharacter(info.Owner, info.OperativeType);
        var character = GameObject.Instantiate(playerPrefab);
        _entities.Add(_idCounter, character);
        character.Init(_idCounter);
        foreach (var comp in dto.InitialComponents)
        {
            character.AddComponent(comp);
        }
        ++_idCounter;
    }
}
