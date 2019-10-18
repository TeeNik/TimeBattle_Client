using System;

public class SkipInput : ActionInput
{
    private readonly Action _show;
    private readonly Action _hide;

    public SkipInput(Action show, Action hide)
    {
        _show = show;
        _hide = hide;
    }

    public ActionType GetActionType()
    {
        return ActionType.Skip;
    }

    public void Update()
    {
    }

    public void ProduceInput()
    {
        Game.I.UserInputController.ProduceInput(ActionType.Skip, null);
        _hide();
    }

    public void WaitForConfirm()
    {
    }

    public void Start(Character ch)
    {
        _show();
    }
}
