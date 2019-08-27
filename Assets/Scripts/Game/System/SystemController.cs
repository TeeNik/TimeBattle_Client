using System;
using System.Collections.Generic;

public class SystemController
{

    public Dictionary<Type, ISystem> Systems { get; private set; }

    public SystemController()
    {
        Systems = new Dictionary<Type, ISystem>();

        Systems.Add(typeof(MovementComponent), new MovementSystem());
        Systems.Add(typeof(ShootComponent), new ShootingSystem());
        Systems.Add(typeof(OperativeInfoCmponent), new OperativeInfoSystem());
    }

    public void UpdateSystems()
    {
    }

    public void ProcessData(int entityId, ActionPhase phase)
    {
        var entity = Game.I.EntityManager.GetEntity(entityId);
        switch (phase.type)
        {
            case ActionType.None:
                break;
            case ActionType.Move:
                entity.GetComponent<MovementComponent>().Update(phase.component);
                break;
            case ActionType.Shoot:
                entity.GetComponent<ShootComponent>().Update(phase.component);
                break;
        }
    }

    public bool IsProcessing()
    {
        return MovementSystem.IsProcessing();
    }
}
