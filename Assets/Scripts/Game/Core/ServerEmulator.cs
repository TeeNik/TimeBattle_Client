using System.Collections.Generic;
using SimpleJSON;

public class ServerEmulator
{

    public void Login()
    {
        JSONObject json = new JSONObject();
        json["cmd"] = "login";
        GameLayer.I.Net.ProcessEvent(json);
    }

    public void Start()
    {
        SendInitialEvent();
    }

    private void SendInitialEvent()
    {
        var list = new List<SpawnEntityDto>() {
            CreateCharacterSpawn(PlayerType.Player1, OperativeType.Assault, new Point(8, 8)),
            CreateCharacterSpawn(PlayerType.Player2, OperativeType.Assault, new Point(9, 4)),

        };
        foreach(var spawn in list)
        {
            Game.I.EntityManager.CreateEntity(spawn);
        }

        Game.I.Messages.SendEvent(EventStrings.OnGameInitialized);
    }

    public void PlayGame()
    {
        JSONObject json = new JSONObject();
        json["cmd"] = "playGame";
        GameLayer.I.Net.ProcessEvent(json);
    }

    private JSONObject CreatePlayGameData()
    {

    }

    private JSONObject CreateCharacterSpawn(PlayerType owner, OperativeType operative, Point point)
    {
        var spawn = new SpawnEntityDto();

    }

    private SpawnEntityDto CreateCharacterSpawn(PlayerType owner, OperativeType operative, Point point)
    {
        var spawn = new SpawnEntityDto();
        var maxHealth = 1;
        spawn.PrefabName = $"{operative}_{owner}";
        spawn.InitialComponents.Add(new ComponentDto());new OperativeInfoCmponent(owner, operative));
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
