using System.Collections.Generic;

public class EntitySpawner
{
    public void StartGame()
    {
        var param = new List<SpawnEntityData>() {
            CreateCharacter(PlayerType.Player1, OperativeType.Ranger, new Pistol(), new Point(8, 14)),
            CreateCharacter(PlayerType.Player1, OperativeType.Sniper, new SniperRifle(), new Point(2, 2)),
            CreateCharacter(PlayerType.Player1, OperativeType.Ranger, new Shotgun(), new Point(9, 8)),
            CreateCharacter(PlayerType.Player2, OperativeType.Soldier, new Pistol(), new Point(2, 15)),
            CreateCharacter(PlayerType.Player2, OperativeType.Sniper, new SniperRifle(), new Point(4, 9)),
            CreateCharacter(PlayerType.Player2, OperativeType.Ranger, new Shotgun(), new Point(9, 4)),
        };

        foreach (var point in GameLayer.I.GameBalance.GetCoversPoint())
        {
            param.Add(CreateCover(point));
        }
        foreach (var spawn in param)
        {
            Game.I.EntityManager.CreateEntity(spawn);
        }

        Game.I.Messages.SendEvent(EventStrings.OnGameInitialized);
    }


    private SpawnEntityData CreateCharacter(PlayerType owner, OperativeType operative, Weapon weapon, Point point)
    {
        var spawn = new SpawnEntityData();
        var maxHealth = 1;
        var mapType = owner == PlayerType.Player1 ? OnMapType.Player1 : OnMapType.Player2;
        spawn.PrefabName = "CharacterBase";
        spawn.InitialComponents.Add(new OperativeInfoComponent(owner, operative));
        spawn.InitialComponents.Add(new MovementComponent(point, mapType));
        spawn.InitialComponents.Add(new ShootComponent(weapon));
        spawn.InitialComponents.Add(new HealthComponent(maxHealth));
        spawn.InitialComponents.Add(new CharacterActionComponent());
        spawn.InitialComponents.Add(new GrenadeThrowComponent());
        return spawn;
    }

    private SpawnEntityData CreateCover(Point point)
    {
        var spawn = new SpawnEntityData
        {
            PrefabName = "Cover"
        };
        spawn.InitialComponents.Add(new MovementComponent(point, OnMapType.Cover));
        spawn.InitialComponents.Add(new HealthComponent(1));
        return spawn;
    }

    public void CreateGrenade(Point startPos, Point target, List<Point> range)
    {
        var spawn = new SpawnEntityData();
        spawn.PrefabName = "Grenade";
        spawn.InitialComponents.Add(new ExplodableComponent(startPos, target, range));
        Game.I.EntityManager.CreateEntity(spawn);
    }
}
