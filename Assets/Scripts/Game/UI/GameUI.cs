using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject Window;
    [SerializeField] private TMP_Text StateText;

    private readonly EventListener _eventListener = new EventListener();

    public void Init()
    {
        _eventListener.Add(Game.I.Messages.Subscribe(EventStrings.OnPlayerChanged, OnPlayerChanged));
        _eventListener.Add(Game.I.Messages.Subscribe(EventStrings.OnGameStateChanged, OnGameStateChanged));
    }

    private void OnGameStateChanged()
    {
        var isInput = Game.I.GameState == GameState.UserInput;
        SetVisibility(isInput);
    }

    private void OnPlayerChanged()
    {
        StateText.text = Game.I.PlayerType.ToString();
    }

    private void SetVisibility(bool isShow)
    {
        Window.SetActive(isShow);
    }

    void OnDestroy()
    {
        _eventListener.Clear();
    }

}
