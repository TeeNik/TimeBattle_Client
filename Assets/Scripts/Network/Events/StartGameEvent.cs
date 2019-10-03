using Newtonsoft.Json.Linq;
using UnityEngine.SceneManagement;

public class StartGameEvent : BaseEventClass
{
    public StartGameEvent() : base("startGame")
    {
    }

    protected override void HandleResponseImpl(JObject json)
    {
        SceneManager.UnloadSceneAsync("Lobby");
        GameLayer.I.SceneController.LoadScene("Game", true, null);
    }
}
