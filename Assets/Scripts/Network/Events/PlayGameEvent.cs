using Newtonsoft.Json.Linq;
using UnityEngine.SceneManagement;

public class PlayGameEvent : BaseEventClass
{
    public PlayGameEvent() : base("playGame")
    {
    }

    public string Send()
    {
        return Json.ToString();
    }

    protected override void HandleResponseImpl(JObject json)
    {
        //SceneManager.UnloadSceneAsync("Lobby");
        //GameLayer.I.SceneController.LoadScene("Game", true, null);
    }
}
