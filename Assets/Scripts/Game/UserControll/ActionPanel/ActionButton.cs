using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    public Button Button;
    public TMP_Text Name;
    public TMP_Text EnergyText;
    public GameObject EnergyBadge;

    private Action<ActionType> _onClick;

    public ActionType ActionType { get; private set; }

    public void Init(ActionConfigData data, Action<ActionType> onClick)
    {
        ActionType = data.type;
        _onClick = onClick;
        Button.onClick.AddListener(OnClick);
        Name.text = data.name;
        EnergyText.text = data.energy.ToString();

        EnergyBadge.SetActive(data.energy > 1);
    }

    public void SetAvailable(bool value)
    {
        
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
