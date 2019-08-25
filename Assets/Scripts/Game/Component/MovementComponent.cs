using System;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : ComponentBase
{
    public List<Point> Positions;

    public MovementComponent(List<Point> positions)
    {
        Positions = positions;
    }
}
