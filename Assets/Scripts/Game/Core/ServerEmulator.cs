using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ServerEmulator
{

    private int _entityCounter = 0;

    public void Login()
    {
        GameLayer.I.Net.ProcessEvent(CreateEventMessage("login", null));
    }

    public void Start()
    {
        SendInitialEvent();
    }

    private JObject CreateEventMessage(string cmd, object param)
    {
        var json = new JObject
        {
            ["cmd"] = cmd,
            ["params"] = JsonUtility.ToJson(param)
        };
        return json;
    }

    private void SendInitialEvent()
    {
        var param = new List<SpawnEntityDto>() {
            CreateCharacterSpawn(PlayerType.Player1, OperativeType.Assault, new Point(8, 8)),
            CreateCharacterSpawn(PlayerType.Player2, OperativeType.Assault, new Point(9, 4)),
        };

        //JObject startGame = CreateEventMessage("startGame", param);
        //GameLayer.I.Net.ProcessEvent(startGame);

        foreach (var spawn in param)
        {
            Game.I.EntityManager.CreateEntity(spawn);
        }

        Game.I.Messages.SendEvent(EventStrings.OnGameInitialized);
    }

    public void PlayGame()
    {
        GameLayer.I.Net.ProcessEvent(CreateEventMessage("playGame", null));
    }

    private SpawnEntityDto CreateCharacterSpawn(PlayerType owner, OperativeType operative, Point point)
    {
        var spawn = new SpawnEntityDto();
        spawn.Id = _entityCounter;
        ++_entityCounter;
        var maxHealth = 1;
        spawn.PrefabName = $"{operative}_{owner}";
        spawn.InitialComponents.Add(new OperativeInfoComponent(owner, operative));
        spawn.InitialComponents.Add(new MovementComponent(point));
        spawn.InitialComponents.Add(new ShootComponent(null));
        spawn.InitialComponents.Add(new HealthComponent(maxHealth));
        spawn.InitialComponents.Add(new CharacterActionComponent());
        return spawn;
    }

    public void SendNextTurn()
    {

    }
}
