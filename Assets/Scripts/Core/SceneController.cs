﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController
{
    public void LoadScene(string sceneName, bool additive, Action<string> onLoaded)
    {
        GameLayer.I.StartCoroutine(LoadSceneImpl(sceneName, additive, onLoaded));
    }

    private IEnumerator LoadSceneImpl(string sceneName, bool additive, Action<string> onLoaded)
    {
        GameLayer.I.LoadingScreen.Show();
        yield return new WaitForSeconds(LoadingScreen.Time);
        var mode = additive ? LoadSceneMode.Additive : LoadSceneMode.Single;
        var operation = SceneManager.LoadSceneAsync(sceneName, mode);
        while (!operation.isDone)
        {
            yield return null;
        }
        onLoaded?.Invoke(sceneName);
        GameLayer.I.LoadingScreen.Hide();
    }

    public void LoadGameScene(PlayerType player = PlayerType.Player1)
    {
        LoadScene("Game", true, (_) =>
        {
            if (!GameLayer.I.EmulateServer)
            {
                Game.I.PlayerType = player;
            }
            Game.I.Messages.SendEvent(EventStrings.OnGameInitialized);
        });
    }

    public void LoadLobbyScene()
    {
        GameLayer.I.SceneController.LoadScene("Lobby", true, null);
    }
}
