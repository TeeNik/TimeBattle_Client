
public enum PlayerType
{
    Player1,
    Player2
}

[Component(ComponentType.OperativeInfo)]
public class OperativeInfoComponent : ComponentBase
{
    public PlayerType Owner;
    public OperativeType OperativeType;

    public OperativeInfoComponent(PlayerType owner, OperativeType operativeType)
    {
        Owner = owner;
        OperativeType = operativeType;
    }

    public override void Update(ComponentBase newData)
    {
    }
}
