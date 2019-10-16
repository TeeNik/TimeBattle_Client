using TMPro;
using UnityEngine;

public class WinnerPanelController : MonoBehaviour
{
    public TMP_Text WinnerText;
    public GameObject Panel;

    private EventListener _eventListener;
    private readonly Color VictoryColor = Utils.CreateColor(190, 47, 44);
    private readonly Color LoseColor = Utils.CreateColor(44, 81, 190);

    void Start()
    {
        _eventListener =new EventListener();
        _eventListener.Add(Game.I.Messages.Subscribe<PlayerWinMsg>(OnPlayerWin));
    }

    private void OnPlayerWin(PlayerWinMsg msg)
    {
        Panel.SetActive(true);
        var isWin = Game.I.PlayerType == msg.Winner;
        WinnerText.text = isWin ? "Victory" : "Lose";
        WinnerText.color = isWin ? VictoryColor : LoseColor;
    }

    void OnDestroy()
    {
        _eventListener.Clear();
    }
}
