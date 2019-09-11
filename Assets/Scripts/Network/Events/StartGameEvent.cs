using Newtonsoft.Json.Linq;

public class StartGameEvent : BaseEventClass
{
    public StartGameEvent(string name) : base(name)
    {
    }

    protected override void HandleResponseImpl(JObject json)
    {
    }
}
