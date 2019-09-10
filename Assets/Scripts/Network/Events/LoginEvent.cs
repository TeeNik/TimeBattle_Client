using SimpleJSON;

public class LoginEvent : BaseEventClass
{
    public LoginEvent() : base(EventNames.Login)
    {
    }

    protected override void HandleResponseImpl(JSONObject json)
    {
        GameLayer.I.SceneController.LoadScene("Lobby", true, null);
    }
}
