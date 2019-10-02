using Newtonsoft.Json.Linq;

public class PlayerTurnEvent : BaseEventClass
{
    public PlayerTurnEvent(string name) : base(name)
    {
    }

    protected override void HandleResponseImpl(JObject json)
    {
        
    }
}
