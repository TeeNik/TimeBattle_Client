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

    public void CreatePlayer()
    {
        var playerPrefab = ResourceManager.Instance.CharacterPrefab;
        var character = GameObject.Instantiate(playerPrefab);
        character.Init(_idCounter);
        Entities.Add(_idCounter, character);
        var moveComp = new MovementComponent(true, new List<Point>() {new Point(8,8)});
        Game.I.SystemController.MovementSystem.AddComponent(_idCounter, moveComp);

        ++_idCounter;
    }
}