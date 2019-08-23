public interface ActionInput
{
    ActionType GetActionType();
    void Update(Character ch);
    void ProduceInput();

}
