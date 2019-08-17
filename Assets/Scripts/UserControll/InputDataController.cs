using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AvailableActions
{
    Move,
}

public class InputDataController : MonoBehaviour
{

    public Dictionary<int, List<AvailableActions>> CurrentActions;

    private Character _selectedChar;


    public void SelectCharacter(Character ch)
    {
        _selectedChar = ch;
    }
}
