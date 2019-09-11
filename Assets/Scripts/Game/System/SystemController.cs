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
            {typeof(ShootComponent), new ShootingSystem()},
            {typeof(MovementComponent), new MovementSystem()},
            {typeof(OperativeInfoCmponent), new OperativeInfoSystem()},
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

        GetSystem<ShootingSystem>().OnUpdateEnd();
    }

    public void ProcessData(int entityId, ComponentBase comp)
    {
        /*var entity = Game.I.EntityManager.GetEntity(entityId);
        var comp = Utils.ActionTypeToComponent(T);
        entity.GetEcsComponent(comp).Update(phase.component);*/
    }

    public bool IsProcessing()
    {
         return Systems.Values.Any(s=>s.IsProcessing());
    }


    public void Dispose()
    {
        foreach (var system in Systems.Values.Where(s=>s is IDisposable).Cast<IDisposable>())
        {
            system.Dispose();
        }
    }
}
