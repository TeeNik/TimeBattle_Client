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

    [SerializeField]
    private GameObject ConfirmPanel;

    private ActionInput _selectedInput;
    private Dictionary<ActionType, ActionInput> _actionInputs;
    private List<ActionButton> _actionButtons;
    private Character _selectedChar;
    private bool _isWaitForConfirm;

    void Start()
    {
        CreateButtons();
        InitActionInputs();
    }

    public void ShowActionPanel(Character ch)
    {
        var ac = ch.GetEcsComponent<CharacterActionComponent>();
        var all = ac.AllActions;

        _selectedChar = ch;
        ActionPanel.SetActive(true);
        SelectionTarget.position = ch.transform.position;

        //TODO Show only available
        foreach (var button in _actionButtons)
        {
            button.SetVisibility(all.Contains(button.ActionType));
        }
    }

    public void HideActionPanel()
    {
        _selectedChar = null;
        ActionPanel.SetActive(false);
    }

    private void SelectAction(ActionType type)
    {
        _selectedInput = _actionInputs[type];
        _selectedInput.Start(_selectedChar);
    }

    private void Update()
    {
        if (_selectedInput != null && !_isWaitForConfirm)
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

    public void ShowConfirmationPanel()
    {
        _isWaitForConfirm = true;
        ConfirmPanel.SetActive(true);
    }

    private void CloseConfirm()
    {
        ConfirmPanel.SetActive(false);
        _selectedInput = null;
        HideActionPanel();
    }

    public void ConfirmAction()
    {
        _selectedInput.ProduceInput();
        CloseConfirm();
    }

    public void CancelAction()
    {
        CloseConfirm();
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
        _actionInputs.Add(ActionType.Shoot, new ShootInput(PredictionMap));
    }

    public void ClearPrediction()
    {
        PredictionMap.ClearPrediction();
    }
}