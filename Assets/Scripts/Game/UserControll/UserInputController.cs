using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ActionType
{
    None = 0,
    Move,
    Shoot,
    ThrowGrenade,
    Skip,
}

public class UserInputController : MonoBehaviour
{

    public Dictionary<int, List<ActionType>> CurrentActions;

    public CharacterActionController ActionController;

    private readonly List<Entity> _currentChars = new List<Entity>();
    private Character _selectedChar;

    private readonly List<ActionPhase> _storedInputs = new List<ActionPhase>();

    private readonly EventListener _eventListener = new EventListener();
    private OperativeInfoSystem _infoSystem;

    public void Init()
    {
        _eventListener.Add(Game.I.Messages.Subscribe(EventStrings.OnNextTurn, OnNextTurn));
        _eventListener.Add(Game.I.Messages.Subscribe(EventStrings.OnGameInitialized, OnGameStarted));
        _infoSystem = Game.I.SystemController.GetSystem<OperativeInfoSystem>();
    }

    private void OnGameStarted()
    {
        SelectPlayerCharacters();
    }

    private void OnNextTurn()
    {
        SelectPlayerCharacters();
    }

    private void SelectPlayerCharacters()
    {
        _currentChars.Clear();
        var player = Game.I.PlayerType;
        var entities = Game.I.EntityManager;
        foreach (var id in _infoSystem.GetEntitiesByOwner(player))
        {
            _currentChars.Add(entities.GetEntity(id));
        }
    }

    public void ReleaseCharacter()
    {
        _selectedChar = null;
        ActionController.HideActionPanel();
    }

    public void SelectCharacter(Character ch)
    {
        if (!_currentChars.Contains(ch) || ch.GetEcsComponent<CharacterActionComponent>().Energy <= 0)
        {
            return;
        }

        if (_selectedChar == ch)
        {
            ReleaseCharacter();
        }
        else
        {
            _selectedChar = ch;
            ActionController.ShowActionPanel(ch);
        }
    }


    public void ProduceInput(ActionType actionType, ComponentBase component)
    {
        if (actionType != ActionType.Skip)
        {
            AddComponentToInput(component);
        }
        RemoveActionFromCharacter(component);
    }

    private void RemoveActionFromCharacter(ComponentBase component)
    {
        Game.I.Messages.SendEvent(new EnergyChangeMsg(_selectedChar.Id, GetComponentLength(component)));
        ReleaseCharacter();
        CheckEndTurn();
    }

    private int GetComponentLength(ComponentBase component)
    {
        if (component != null)
        {
            return component.GetUpdateLength();
        }
        return 10;
    }

    private void AddComponentToInput(ComponentBase comp)
    {
        var action = _storedInputs.Find(a => a.entityId == _selectedChar.Id);
        if (action == null)
        {
            action = new ActionPhase()
            {
                entityId = _selectedChar.Id,
                dtos = new List<ComponentDto>()
            };
            _storedInputs.Add(action);
        }
        var fullTime = action.dtos.Sum(c => GetComponentLength(c.ToComponentBase()));
        action.dtos.Add(new ComponentDto(comp, fullTime));
    }

    //TODO this function will be changed
    private void CheckEndTurn()
    {
        var isEnd = _currentChars.All(c => c.GetEcsComponent<CharacterActionComponent>().Energy <= 0);
        if (isEnd)
        {
            if (GameLayer.I.EmulateServer)
            {
                var prev = Game.I.PlayerType;
                Game.I.PlayerType = Utils.GetOppositePlayer(Game.I.PlayerType);
                Debug.Log($"{prev.ToString()} ended. {Game.I.PlayerType} is started.");
                Game.I.Messages.SendEvent(EventStrings.OnPlayerChanged);
                if (prev == PlayerType.Player2)
                {
                    ProcessTurn(_storedInputs);
                }
                else
                {
                    SelectPlayerCharacters();
                }
            }
            else
            {
                GameLayer.I.Net.SendPlayerTurn(_storedInputs);
            }

        }
    }

    public void ProcessTurn(List<ActionPhase> turnData)
    {
        Debug.Log("ProcessTurn");
        ActionController.PredictionMap.ClearAll();
        Game.I.OnTurnData(turnData);
        _storedInputs.Clear();
    }

    void OnDestroy()
    {
        _eventListener.Clear();
    }
}
