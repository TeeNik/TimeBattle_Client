using Newtonsoft.Json.Linq;

public class LoginEvent : BaseEventClass
{
    public LoginEvent() : base(EventNames.Login)
    {
    }

    protected override void HandleResponseImpl(JObject json)
    {
        GameLayer.I.SceneController.LoadScene("Lobby", true, null);
    }
}
