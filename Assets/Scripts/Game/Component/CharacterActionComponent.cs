using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterActionComponent : ComponentBase
{

    public List<ActionType> ReusableActions = new List<ActionType>();
    public List<ActionType> DisposableActions = new List<ActionType>();

    public List<ActionType> AllActions => ReusableActions.Union(DisposableActions).ToList();

    public void Update(ComponentBase newData)
    {
        var cc = (CharacterActionComponent) newData;
        ReusableActions = cc.ReusableActions;
        DisposableActions = cc.DisposableActions;
    }
}
