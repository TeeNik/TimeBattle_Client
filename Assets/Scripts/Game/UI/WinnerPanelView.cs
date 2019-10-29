using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WinnerPanelView : MonoBehaviour
{
    public TMP_Text WinnerText;
    public GameObject Panel;
    public Button ReplayButton;
    public Button ExitButton;

    private readonly Color VictoryColor = Utils.CreateColor(190, 47, 44);
    private readonly Color LoseColor = Utils.CreateColor(44, 81, 190);

    public void Show(bool isWin, UnityAction replay, UnityAction exit)
    {
        ReplayButton.onClick.AddListener(replay);
        ExitButton.onClick.AddListener(exit);
        Panel.SetActive(true);
        WinnerText.text = isWin ? "Victory" : "Lose";
        WinnerText.color = isWin ? VictoryColor : LoseColor;
    }

}
