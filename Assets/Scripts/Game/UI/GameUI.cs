using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject Window;
    [SerializeField] private TMP_Text StateText;
    [SerializeField] private WinnerPanelView WinnerPanel;

    private readonly EventListener _eventListener = new EventListener();

    public void Init()
    {
        _eventListener.Add(Game.I.Messages.Subscribe(EventStrings.OnPlayerChanged, OnPlayerChanged));
        _eventListener.Add(Game.I.Messages.Subscribe(EventStrings.OnGameStateChanged, OnGameStateChanged));
        _eventListener.Add(Game.I.Messages.Subscribe<PlayerWinMsg>(OnPlayerWinMsg));
    }

    private void OnPlayerWinMsg(PlayerWinMsg msg)
    {
        var isWin = Game.I.PlayerType == msg.Winner;
        WinnerPanel.Show(isWin, Replay, Exit);
    }

    private void Exit()
    {
        SceneManager.UnloadSceneAsync("Game");
        GameLayer.I.SceneController.LoadLobbyScene();
    }

    private void Replay()
    {
        SceneManager.UnloadSceneAsync("Game");
        GameLayer.I.SceneController.LoadGameScene(Game.I.PlayerType);
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
