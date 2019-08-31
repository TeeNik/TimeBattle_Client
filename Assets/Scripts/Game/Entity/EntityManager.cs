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

    public void CreatePlayer(SpawnEntityDto dto)
    {
        var playerPrefab = ResourceManager.Instance.CharacterPrefab;
        var character = GameObject.Instantiate(playerPrefab);
        var initial = new List<ComponentBase>{dto.operativeInfo, dto.spawnPosition};
        Entities.Add(_idCounter, character);
        character.AddComponent(dto.operativeInfo);
        character.AddComponent(dto.spawnPosition);

        ++_idCounter;
    }
}
