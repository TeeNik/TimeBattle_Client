using System.Collections.Generic;

public class CharacterActionSystem : ISystem
{

    protected Dictionary<int, CharacterActionComponent> Components = new Dictionary<int, CharacterActionComponent>();

    public void AddComponent(Entity entity, ComponentBase component)
    {
        var cc = (CharacterActionComponent) component;
        Components.Add(entity.Id, cc);
        CreateReusableActions(cc);
    }

    public void RemoveComponent(int entityId)
    {
        Components.Remove(entityId);
    }

    private void CreateReusableActions(CharacterActionComponent comp)
    {
        comp.ReusableActions.Clear();
        comp.ReusableActions.AddRange(new[] {ActionType.Move, ActionType.Move, ActionType.Shoot});
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
