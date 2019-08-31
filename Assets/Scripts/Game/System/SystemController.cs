using System;
using System.Collections.Generic;
using System.Linq;

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
        foreach(var system in Systems.Values)
        {
            system.Update();
        }
    }

    public void ProcessData(int entityId, ActionPhase phase)
    {
        var entity = Game.I.EntityManager.GetEntity(entityId);
        switch (phase.type)
        {
            case ActionType.None:
                break;
            case ActionType.Move:
                entity.GetEcsComponent<MovementComponent>().Update(phase.component);
                break;
            case ActionType.Shoot:
                entity.GetEcsComponent<ShootComponent>().Update(phase.component);
                break;
        }
    }

    public bool IsProcessing()
    {
        return Systems.Values.Any(s => s.IsProcessing());
    }
}
