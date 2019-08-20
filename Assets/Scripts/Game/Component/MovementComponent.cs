using UnityEngine;

public class MovementComponent : ComponentBase
{
    public readonly bool IsInitial;
    public readonly Vector3Int[] Positions;

    public MovementComponent(bool isInitial, Vector3Int[] positions)
    {
        IsInitial = isInitial;
        Positions = positions;
    }
}
