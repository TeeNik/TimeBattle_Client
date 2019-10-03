using System;
public class FlagController : IDisposable
{
    private EventListener _eventListener = new EventListener();

    public FlagController()
    {
        _eventListener = new EventListener();
        //_eventListener.Add(Game.I.Messages.Subscribe());
        Game.I.SystemController.Systems.Add(typeof(FlagCarryComponent), new FlagCarringSystem());

    }

    public void Setup()
    {
    }

    public void Dispose()
    {
        _eventListener.Clear();
    }
}
