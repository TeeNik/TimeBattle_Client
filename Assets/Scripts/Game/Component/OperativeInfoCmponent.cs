
public enum PlayerType
{
    Player1,
    Player2
}

public class OperativeInfoCmponent : ComponentBase
{
    public PlayerType Owner;
    public OperativeType OperativeType;

    public OperativeInfoCmponent(PlayerType owner, OperativeType operativeType)
    {
        Owner = owner;
        OperativeType = operativeType;
    }
}
