using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameEvent : BaseEventClass
{
    public StartGameEvent() : base("startGame")
    {
    }

    protected override void HandleResponseImpl(JObject json)
    {
        SceneManager.UnloadSceneAsync("Lobby");
        GameLayer.I.SceneController.LoadScene("Game", true, (_)=>
        {
            if (!GameLayer.I.EmulateServer)
            {
                var player = json["playerType"].ToObject<PlayerType>();
                Game.I.PlayerType = player;
            }
            Game.I.Messages.SendEvent(EventStrings.OnGameInitialized);
        });
    }
}
