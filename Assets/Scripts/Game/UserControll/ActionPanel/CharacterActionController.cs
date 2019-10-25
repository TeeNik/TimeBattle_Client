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

    public PredictionMap PredictionMap;

    [SerializeField]
    private ActionButton ActionButtonPrefab;

    [SerializeField] private GameObject ConfirmPanel;
    public ShootConfirmPanel ShootConfirmPanel;

    private ActionInput _selectedInput;
    private Dictionary<ActionType, ActionInput> _actionInputs;
    private List<ActionButton> _actionButtons;
    private Character _selectedChar;
    private bool _isWaitForConfirm;
    private readonly Vector3  HidePos = new Vector3(10000,10000);

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
        SelectionTarget.position = HidePos;
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
                _selectedInput.Update();
                _isWaitForConfirm = true;
                _selectedInput.WaitForConfirm();
            }
            else
            {
                //_selectedInput.Update();
            }
        }
    }

    public void ShowConfirmation()
    {
        ConfirmPanel.SetActive(true);
    }

    public void ShowShootConfirm(int min, int max)
    {
        ShootConfirmPanel.Show(min, max);
    }

    public void CloseConfirm()
    {
        Game.I.UserInputController.ReleaseCharacter();
        PredictionMap.ClearLayer(Layers.Temporary);
        Game.I.MapController.OutlinePool.ReturnAll();
        ConfirmPanel.SetActive(false);
        ShootConfirmPanel.Hide();
        _selectedInput = null;
        HideActionPanel();
        _isWaitForConfirm = false;
    }

    public void ConfirmAction()
    {
        _selectedInput.ProduceInput();
        CloseConfirm();
    }

    private void CreateButtons()
    {
        _actionButtons = new List<ActionButton>();
        var config = Utils.ParseConfig<ActionConfig>("actions");
        foreach (var data in config.actions)
        {
            var button = Instantiate(ActionButtonPrefab, ActionPanel.transform);
            button.Init(data, SelectAction);
            _actionButtons.Add(button);
        }
    }

    private void InitActionInputs()
    {
        _actionInputs = new Dictionary<ActionType, ActionInput>
        {
            {ActionType.Move, new MoveInput(ShowConfirmation, CloseConfirm)},
            {ActionType.Shoot, new ShootInput(ShowShootConfirm, CloseConfirm, ShootConfirmPanel.GetValue)},
            {ActionType.ThrowGrenade, new ThrowGrenadeInput(ShowConfirmation, CloseConfirm)},
            {ActionType.Skip, new SkipInput(ShowConfirmation, CloseConfirm)}
        };
    }
}