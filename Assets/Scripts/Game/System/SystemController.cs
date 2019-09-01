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
        Systems.Add(typeof(HealthComponent), new HealthSystem());
    }

    public void UpdateSystems()
    {
        foreach (var system in Systems.Values)
        {
            system.Update();
        }
    }

    public void ProcessData(int entityId, ActionPhase phase)
    {
        var entity = Game.I.EntityManager.GetEntity(entityId);
        var comp = Utils.ActionTypeToComponent(phase.type);
        entity.GetEcsComponent(comp).Update(phase.component);
    }

    public bool IsProcessing()
    {
        return Systems.Values.Any(s=>s.IsProcessing());
    }
}
