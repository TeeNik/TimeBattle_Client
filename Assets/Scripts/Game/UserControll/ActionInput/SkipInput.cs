using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipInput : ActionInput
{

    public ActionType GetActionType()
    {
        return ActionType.Skip;
    }

    void ActionInput.Update()
    {
        Update();
    }

    public void ProduceInput()
    {
        throw new System.NotImplementedException();
    }

    public void WaitForConfirm()
    {
        throw new System.NotImplementedException();
    }

    public void Start(Character ch)
    {
        throw new System.NotImplementedException();
    }

    void Update()
    {
        
    }
}
