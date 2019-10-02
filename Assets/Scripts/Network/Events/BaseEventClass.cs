using Newtonsoft.Json.Linq;

public abstract class BaseEventClass
{

    public readonly string Header;

    protected JObject Json;

    protected BaseEventClass(string name)
    {
        Header = name;
        Json = new JObject {["cmd"] = name};
    }

    public void HandleResponse(JObject json)
    {
        HandleResponseImpl(json);
    }

    protected abstract void HandleResponseImpl(JObject json);

}
