using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    [SerializeField]
    private TMPro.TMP_Text _text;

    public ActionType ActionType { get; private set; }

    public void Init(ActionType type)
    {
        ActionType = type;
    }

}
