public class HealthSystem : ISystem
{
    private EventListener _eventListener;

    public HealthSystem()
    {
        _eventListener = new EventListener();
        _eventListener.Add(Game.I.Messages.Subscribe<TakeDamageMsg>(OnTakeDamageMsg));
    }

    private void OnTakeDamageMsg(TakeDamageMsg msg)
    {
        var entity = (Character)Game.I.EntityManager.GetEntity(msg.EntityId);
    }

    public void Update()
    {
        
    }

    public void AddComponent(int entityId, ComponentBase component)
    {
        
    }

    public bool IsProcessing()
    {
        return false;
    }
}
