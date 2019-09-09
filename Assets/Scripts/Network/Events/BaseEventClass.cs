using SimpleJSON;

public abstract class BaseEventClass
{

    public readonly string Header;

    protected BaseEventClass(string name)
    {
        Header = name;
    }

    public void HandleResponse(JSONObject json)
    {
        HandleResponseImpl(json);
    }

    protected abstract void HandleResponseImpl(JSONObject json);

}
