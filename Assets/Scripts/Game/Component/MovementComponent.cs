using System;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : ComponentBase
{
    public bool IsInitial;
    public List<Point> Positions;

    public MovementComponent(bool isInitial, List<Point> positions)
    {
        IsInitial = isInitial;
        Positions = positions;
    }
}
