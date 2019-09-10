using SimpleJSON;
using UnityEngine.SceneManagement;

public class PlayGameEvent : BaseEventClass
{
    public PlayGameEvent() : base("playGame")
    {
    }

    protected override void HandleResponseImpl(JSONObject json)
    {
        SceneManager.UnloadSceneAsync("Lobby");
        GameLayer.I.SceneController.LoadScene("Game", true, null);
    }
}
