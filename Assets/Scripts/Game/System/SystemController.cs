public class SystemController
{

    public readonly MovementSystem MovementSystem;
    public readonly PositionSystem PositionSystem;

    public SystemController()
    {
        MovementSystem = new MovementSystem();
        PositionSystem = new PositionSystem();
    }

    public void UpdateSystems()
    {
        MovementSystem.Update();
    }

    public void ProcessData(int entityId, ActionPhase phase)
    {
        switch (phase.type)
        {
            case ActionType.None:
                break;
            case ActionType.Move:
                MovementSystem.AddComponent(entityId, (MovementComponent)phase.component);
                break;
            case ActionType.Shoot:
                break;
        }
    }

    public bool IsProcessing()
    {
        return MovementSystem.IsProcessing();
    }
}
