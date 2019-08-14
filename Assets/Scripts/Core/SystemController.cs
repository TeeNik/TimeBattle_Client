public class SystemController
{

    public readonly MovementSystem MovementSystem;

    public SystemController()
    {
        MovementSystem = new MovementSystem();
    }

    public void UpdateSystems()
    {
        MovementSystem.Update();
    }
}
