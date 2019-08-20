public class MoveInput : ActionInput
{
    private PredictionMap _prediction;

    public MoveInput(PredictionMap prediction)
    {
        _prediction = prediction;
    }

    public void ProduceInput()
    {
        _prediction.DrawCharacter();
    }

    public void Update()
    {
        _prediction.DrawPath();
    }

    ActionType ActionInput.GetType()
    {
        return ActionType.Move;
    }
}
