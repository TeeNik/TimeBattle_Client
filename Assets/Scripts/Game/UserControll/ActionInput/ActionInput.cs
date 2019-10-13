public interface ActionInput
{
    ActionType GetActionType();
    void Update();
    void ProduceInput();
    void WaitForConfirm();
    void Start(Character ch);

}
