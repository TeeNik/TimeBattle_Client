﻿using System;
using System.Collections.Generic;

[Component(ComponentType.Shoot)]
public class ShootComponent : ComponentBase
{
    public List<Point> Range;

    [NonSerialized]
    public Weapon Weapon;

    public int Time;

    public ShootComponent()
    {

    }

    public ShootComponent(Weapon weapon)
    {
        Weapon = weapon;
    }

    public ShootComponent(List<Point> range, int duration)
    {
        Range = range;
        Time = duration;
    }

    public override void Update(ComponentBase newData)
    {
        var sc = (ShootComponent)newData;
        if(sc.Range != null)
        {
            Range = sc.Range;
            Time = sc.Time;
        }
    }

    public override int GetUpdateLength()
    {
        return Time;
    }
}
