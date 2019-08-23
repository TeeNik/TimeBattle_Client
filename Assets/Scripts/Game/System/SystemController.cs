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

    public int GetMaxPhaseLength()
    {
        return MovementSystem.GetPhaseLength();
    }
}
