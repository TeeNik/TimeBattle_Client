public interface ActionInput
{
    ActionType GetActionType();
    void Update();
    void ProduceInput();
    void Start(Character ch);

}
