using Newtonsoft.Json.Linq;

public class StartGameEvent : BaseEventClass
{
    public StartGameEvent() : base("startGame")
    {
    }

    protected override void HandleResponseImpl(JObject json)
    {
    }
}
