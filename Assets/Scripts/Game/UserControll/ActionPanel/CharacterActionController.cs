using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterActionController : MonoBehaviour
{

    [SerializeField]
    private GameObject ActionPanel;

    [SerializeField]
    private Transform SelectionTarget;

    [SerializeField]
    private PredictionMap PredictionMap;

    [SerializeField]
    private ActionButton ActionButtonPrefab;

    private ActionInput _selectedInput;
    private Dictionary<ActionType, ActionInput> _actionInputs;
    private List<ActionButton> _actionButtons;

    void Start()
    {
        CreateButtons();
        InitActionInputs();
    }

    public void ShowActionPanel(Character ch)
    {
        ActionPanel.SetActive(true);
        SelectionTarget.position = ch.transform.position;


        //TODO Show only available
        foreach (var button in _actionButtons)
        {
            button.SetVisibility(true);
        }
    }

    public void HideActionPanel()
    {
        ActionPanel.SetActive(false);
    }

    private void SelectAction(ActionType type)
    {
        _selectedInput = _actionInputs[type];
    }

    private void Update()
    {
        if (_selectedInput != null)
        {
            if (Input.GetMouseButton(0))
            {
                _selectedInput.ProduceInput();
                _selectedInput = null;
                HideActionPanel();
            }
            else
            {
                _selectedInput.Update();
            }
        }
    }

    private void CreateButtons()
    {
        _actionButtons = new List<ActionButton>();
        foreach (var type in Enum.GetValues(typeof(ActionType)).Cast<ActionType>())
        {
            if (type != ActionType.None)
            {
                var button = Instantiate(ActionButtonPrefab, ActionPanel.transform);
                button.Init(type, SelectAction);
                _actionButtons.Add(button);
            }
        }
    }

    private void InitActionInputs()
    {
        _actionInputs = new Dictionary<ActionType, ActionInput>();

        _actionInputs.Add(ActionType.Move, new MoveInput(PredictionMap));
    }
}