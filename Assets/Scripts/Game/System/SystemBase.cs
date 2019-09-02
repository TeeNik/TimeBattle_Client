public interface ISystem
{
    void Update();
    void AddComponent(Entity entity, ComponentBase component);
    void RemoveComponent(int entityId);
    bool IsProcessing();
}
