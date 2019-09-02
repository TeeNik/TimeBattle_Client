using System.Collections.Generic;
using UnityEngine;

public class EntityManager
{

    public Dictionary<int, Entity> Entities;
    private static int _idCounter = 0;

    public EntityManager()
    {
        Entities = new Dictionary<int, Entity>();
    }

    public Entity GetEntity(int id)
    {
        return Entities[id];
    }

    public void DestroyEntity(int entityId)
    {
        var entity = Entities[entityId];
        entity.ClearComponents();
        Entities.Remove(entityId);
        GameObject.Destroy(entity.gameObject);
    }

    public void CreatePlayer(SpawnEntityDto dto)
    {
        var playerPrefab = ResourceManager.Instance.CharacterPrefab;
        var character = GameObject.Instantiate(playerPrefab);
        Entities.Add(_idCounter, character);
        character.Init(_idCounter);
        foreach (var comp in dto.InitialComponents)
        {
            character.AddComponent(comp);
        }
        ++_idCounter;
    }
}
