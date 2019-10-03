using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIController : MonoBehaviour
{
    [SerializeField] private Button _playButton;

    void Start()
    {
        _playButton.onClick.AddListener(Play);      
    }

    void Play()
    {
        if (GameLayer.I.EmulateServer)
        {
            GameLayer.I.ServerEmulator.PlayGame();
        }
        else
        {
            GameLayer.I.Net.PlayGame();
        }
    }
}
