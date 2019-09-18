using System.Collections.Generic;
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

    private int GetId()
    {
        return _entityCounter++;
    }

    private void SendInitialEvent()
    {
        var param = new List<SpawnEntityDto>() {
            //CreateCharacter(PlayerType.Player1, OperativeType.Assault, new Point(8, 8)),
            //CreateCharacter(PlayerType.Player1, OperativeType.Assault, new Point(2, 2)),
            CreateCharacter(PlayerType.Player1, OperativeType.Assault, new Point(4, 2)),
            //CreateCharacter(PlayerType.Player2, OperativeType.Assault, new Point(1, 9)),
            CreateCharacter(PlayerType.Player2, OperativeType.Assault, new Point(4, 9)),
            //CreateCharacter(PlayerType.Player2, OperativeType.Assault, new Point(9, 4)),
            CreateCover(new Point(5, 7)),
            CreateCover(new Point(5, 8)),
            CreateCover(new Point(5, 9)),
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

    private SpawnEntityDto CreateCharacter(PlayerType owner, OperativeType operative, Point point)
    {
        var spawn = new SpawnEntityDto {Id = GetId()};
        var maxHealth = 1;
        var mapType = owner == PlayerType.Player1 ? OnMapType.Player1 : OnMapType.Player2;
        spawn.PrefabName = "CharacterBase";
        spawn.InitialComponents.Add(new OperativeInfoComponent(owner, operative));
        spawn.InitialComponents.Add(new MovementComponent(point, mapType));
        spawn.InitialComponents.Add(new ShootComponent(null));
        spawn.InitialComponents.Add(new HealthComponent(maxHealth));
        spawn.InitialComponents.Add(new CharacterActionComponent());
        return spawn;
    }

    private SpawnEntityDto CreateCover(Point point)
    {
        var spawn = new SpawnEntityDto
        {
            Id = GetId(),
            PrefabName = "Cover"
        };
        spawn.InitialComponents.Add(new MovementComponent(point, OnMapType.Cover));
        spawn.InitialComponents.Add(new HealthComponent(1));
        return spawn;
    }

    public void SendNextTurn()
    {

    }
}
