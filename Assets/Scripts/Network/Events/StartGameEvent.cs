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

        var player = json["playerType"].ToObject<PlayerType>();
        GameLayer.I.SceneController.LoadGameScene(player);

        /*GameLayer.I.SceneController.LoadScene("Game", true, (_)=>
        {
            if (!GameLayer.I.EmulateServer)
            {
                Game.I.PlayerType = player;
            }
            Game.I.Messages.SendEvent(EventStrings.OnGameInitialized);
        });*/
    }
}
