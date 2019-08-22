using System;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : ComponentBase
{
    public bool IsInitial;
    public List<Vector3Int> Positions;

    public MovementComponent(bool isInitial, List<Vector3Int> positions)
    {
        IsInitial = isInitial;
        Positions = positions;
    }
}
