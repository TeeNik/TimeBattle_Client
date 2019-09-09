public abstract class BaseEventClass
{

    public readonly string Header;

    protected BaseEventClass(string name)
    {
        Header = name;
    }

    protected abstract void HandleResponse();

}
