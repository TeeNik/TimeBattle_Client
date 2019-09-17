public class FlagController
{
    private EventListener _eventListener = new EventListener();

    public void Init()
    {
        _eventListener = new EventListener();
        //_eventListener.Add(Game.I.Messages.Subscribe());
        //Game.I.SystemController.Systems.Add();
    }

    public void Setup()
    {
    }
}
