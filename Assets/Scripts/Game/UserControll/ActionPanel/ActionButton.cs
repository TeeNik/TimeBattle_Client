using System;
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

    private Action<ActionType> _onClick;

    public ActionType ActionType { get; private set; }

    public void Init(ActionType type, Action<ActionType> onClick)
    {
        ActionType = type;
        _onClick = onClick;
        _button.onClick.AddListener(OnClick);
        _text.text = type.ToString();
    }

    public void SetVisibility(bool value)
    {
        gameObject.SetActive(value);
    }

    public void OnClick()
    {
        _onClick?.Invoke(ActionType);
    }
}
