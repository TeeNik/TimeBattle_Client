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

    private Character _selectedChar;

    public CharacterActionController ActionController;

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
}
