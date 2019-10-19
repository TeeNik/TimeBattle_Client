using System;
using System.Collections.Generic;

public class CharacterActionSystem : ISystem, IDisposable
{
    private const int MaxEnergy = 10;
    protected Dictionary<int, CharacterActionComponent> Components = new Dictionary<int, CharacterActionComponent>();
    private readonly Dictionary<int, ActionView> _views = new Dictionary<int, ActionView>();
    private readonly EventListener _eventListener;

    public CharacterActionSystem()
    {
        _eventListener = new EventListener();
        _eventListener.Add(Game.I.Messages.Subscribe<EnergyChangeMsg>(OnEnergyChanged));
    }

    public void AddComponent(Entity entity, ComponentBase component)
    {
        var cc = (CharacterActionComponent) component;
        CreateReusableActions(cc);
        Components.Add(entity.Id, cc);
        var view = entity.GetComponent<ActionView>();
        view.SetValue(MaxEnergy);
        _views.Add(entity.Id, view);
    }

    private void OnEnergyChanged(EnergyChangeMsg msg)
    {
        var view = _views[msg.EntityId];
        var component = Components[msg.EntityId];
        component.Energy -= msg.Energy;
        view.SetValue(component.Energy);
    }

    public void RemoveComponent(int entityId)
    {
        Components.Remove(entityId);
        _views.Remove(entityId);
    }

    private void CreateReusableActions(CharacterActionComponent comp)
    {
        comp.ReusableActions.Clear();
        comp.ReusableActions.AddRange(new[] {ActionType.Move, ActionType.Move, ActionType.Shoot, ActionType.ThrowGrenade, ActionType.Skip});
        comp.Energy = MaxEnergy;
    }

    private void CreateDisposableActions(CharacterActionComponent comp)
    {
        //TODO later
    }

    public bool IsProcessing()
    {
        return false;
    }

    public void Update()
    {
        foreach (var component in Components)
        {
            CreateReusableActions(component.Value);
        }
    }

    public void Dispose()
    {
        _eventListener.Clear();
    }
}
