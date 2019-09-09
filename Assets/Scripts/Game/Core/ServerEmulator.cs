using System.Collections.Generic;

public class ServerEmulator
{

    public void Login()
    {

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

    private SpawnEntityDto CreateCharacterSpawn(PlayerType owner, OperativeType operative, Point point)
    {
        var spawn = new SpawnEntityDto();
        var maxHealth = 1;
        spawn.PrefabName = $"{operative}_{owner}";
        spawn.InitialComponents.Add(new OperativeInfoCmponent(owner, operative));
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
