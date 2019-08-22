using System.Collections.Generic;
using UnityEngine;

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
        var tile = Game.I.MapController.GetTileByMouse();
        MovementComponent mc = new MovementComponent(false, new List<Vector3Int>() {tile});
        Game.I.InputController.ProduceInput(GetActionType(), mc);
    }

    public void Update()
    {
        _prediction.DrawPath();
    }

    public ActionType GetActionType()
    {
        return ActionType.Move;
    }
}
