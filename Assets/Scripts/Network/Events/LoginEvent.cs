using SimpleJSON;

public class LoginEvent : BaseEventClass
{
    public LoginEvent() : base(EventNames.Login)
    {
    }

    protected override void HandleResponseImpl(JSONObject json)
    {
        throw new System.NotImplementedException();
    }
}
