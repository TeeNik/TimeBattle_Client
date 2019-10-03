using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SystemController : IDisposable
{

    public Dictionary<Type, ISystem> Systems { get; private set; }

    public SystemController()
    {
        Systems = new Dictionary<Type, ISystem>
        {
            {typeof(MovementComponent), new MovementSystem()},
            {typeof(ShootComponent), new ShootingSystem()},
            {typeof(OperativeInfoComponent), new OperativeInfoSystem()},
            {typeof(HealthComponent), new HealthSystem()},
            {typeof(CharacterActionComponent), new CharacterActionSystem()}
        };

        ComponentBase.GetComponentType(ComponentType.Movement);
    }

    public T GetSystem<T>()
    {
        return (T)Systems.Values.First(s => s is T);
    }

    public void UpdateSystems()
    {
        foreach (var system in Systems.Values)
        {
            system.Update();
        }
    }

    public void OnUpdateEnd()
    {
        GetSystem<ShootingSystem>().OnUpdateEnd();
    }

    public void ProcessData(int entityId, ComponentBase comp)
    {
        var entity = Game.I.EntityManager.GetEntity(entityId);
        if (entity != null)
        {
            entity.GetEcsComponent(comp.GetType()).Update(comp);
        }
    }

    public bool IsProcessing()
    {
        return Systems.Values.Any(s=>s.IsProcessing());
    }

    public int GetPhaseLength()
    {
        return Systems.Values.Max(s => s.GetPhaseLegth());
    }


    public void Dispose()
    {
        foreach (var system in Systems.Values.Where(s=>s is IDisposable).Cast<IDisposable>())
        {
            system.Dispose();
        }
    }
}
