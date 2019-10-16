using System.Collections.Generic;

public class CharacterActionSystem : ISystem
{
    private const int MaxEnergy = 10;
    protected Dictionary<int, CharacterActionComponent> Components = new Dictionary<int, CharacterActionComponent>();
    protected Dictionary<int, ActionView> Views = new Dictionary<int, ActionView>();

    public void AddComponent(Entity entity, ComponentBase component)
    {
        var cc = (CharacterActionComponent) component;
        CreateReusableActions(cc);
        Components.Add(entity.Id, cc);
    }

    public void RemoveComponent(int entityId)
    {
        Components.Remove(entityId);
    }

    private void CreateReusableActions(CharacterActionComponent comp)
    {
        comp.ReusableActions.Clear();
        comp.ReusableActions.AddRange(new[] {ActionType.Move, ActionType.Move, ActionType.Shoot});
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
}
