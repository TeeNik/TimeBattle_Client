using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
    None,
    Move,
    Shoot,
}

public class InputDataController : MonoBehaviour
{

    public Dictionary<int, List<ActionType>> CurrentActions;

    public CharacterActionController ActionController;

    private Character _selectedChar;
    private readonly List<ActionDto> _storedInputs = new List<ActionDto>();

    public void SelectCharacter(Character ch)
    {
        if(_selectedChar == ch)
        {
            _selectedChar = null;
            ActionController.HideActionPanel();
        }
        else
        {
            _selectedChar = ch;
            ActionController.ShowActionPanel(ch);
        }
    }

    public void ProduceInput(ActionType compType, ComponentBase comp)
    {
        var action = _storedInputs.Find(a => a.entityId == _selectedChar.Id);
        if (action == null)
        {
            action = new ActionDto
            {
                entityId = _selectedChar.Id,
                phases = new List<ActionPhase>()
            };
            _storedInputs.Add(action);
        }
        action.phases.Add(new ActionPhase{type = compType, component = comp});

        SelectCharacter(_selectedChar);
    }

    public void LogStoredActions()
    {
        foreach (var input in _storedInputs)
        {
            Debug.Log($"<color=blue>{input.entityId}</color> ");
            foreach (var phase in input.phases)
            {
                var json = JsonUtility.ToJson(phase.component);
                Debug.Log($"{phase.type.ToString()} -- {json.ToString()}");
            }
        }
    }

    public void ProduceSystemUpdate()
    {
        Game.I.OnTurnData(_storedInputs);
        _storedInputs.Clear();
    }
}
