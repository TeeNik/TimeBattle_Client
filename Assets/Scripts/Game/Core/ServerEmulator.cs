public class ServerEmulator
{
    public void Start()
    {
        SendInitialEvent();
    }

    private void SendInitialEvent()
    {
        var spawn1 = CreateSpawnDto(PlayerType.Player1, new Point(8, 8));
        var spawn2 = CreateSpawnDto(PlayerType.Player2, new Point(9, 4));
        Game.I.EntityManager.CreatePlayer(spawn1);
        Game.I.EntityManager.CreatePlayer(spawn2);

        Game.I.Messages.SendEvent(EventStrings.OnGameInitialized);
    }

    private SpawnEntityDto CreateSpawnDto(PlayerType owner, Point point)
    {
        var spawn = new SpawnEntityDto();
        var maxHealth = 1;
        spawn.InitialComponents.Add(new OperativeInfoCmponent(owner, OperativeType.Soldier));
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
