using System;
using System.Collections.Generic;

public class SystemController
{

    //public readonly PositionSystem PositionSystem;
    public readonly OperativeInfoSystem OperativeInfoSystem;

    public Dictionary<Type, ISystem> Systems { get; private set; }

    public SystemController()
    {
        Systems = new Dictionary<Type, ISystem>();

        Systems.Add(typeof(MovementComponent), new MovementSystem());
        Systems.Add(typeof(ShootComponent), new ShootingSystem());

        //PositionSystem = new PositionSystem();
        OperativeInfoSystem = new OperativeInfoSystem();
    }

    public void UpdateSystems()
    {
        MovementSystem.Update();
        ShootingSystem.Update();

        MovementSystem
    }

    public void ProcessData(int entityId, ActionPhase phase)
    {
        switch (phase.type)
        {
            case ActionType.None:
                break;
            case ActionType.Move:
                MovementSystem.AddComponent(entityId, (MovementComponent)phase.component);
                break;
            case ActionType.Shoot:
                ShootingSystem.AddComponent(entityId, (ShootComponent)phase.component);
                break;
        }
    }

    public bool IsProcessing()
    {
        return MovementSystem.IsProcessing();
    }
}
