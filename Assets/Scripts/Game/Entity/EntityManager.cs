﻿using System.Collections.Generic;
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
        character.Init(_idCounter);
        Entities.Add(_idCounter, character);
        Game.I.SystemController.OperativeInfoSystem.AddComponent(_idCounter, dto.operativeInfo);
        Game.I.SystemController.PositionSystem.AddComponent(_idCounter, dto.spawnPosition);

        ++_idCounter;
    }
}
