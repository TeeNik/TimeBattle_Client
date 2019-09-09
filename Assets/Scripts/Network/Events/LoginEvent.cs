public class LoginEvent : BaseEventClass
{
    public LoginEvent() : base(EventNames.Login)
    {
    }

    protected override void HandleResponse()
    {
        throw new System.NotImplementedException();
    }
}
