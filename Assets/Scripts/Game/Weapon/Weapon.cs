using System.Collections.Generic;
using System.Linq;

public enum WeaponType
{
    Pistol,
    Rifle,
}

public abstract class Weapon
{
    public WeaponType Type;

    protected abstract List<Point> Distance { get; }
    public List<Point> Left {get; private set;}
    public List<Point> Right {get; private set;}
    public List<Point> Up  {get; private set;}
    public List<Point> Down  {get; private set;}

    public Weapon()
    {
        Down = Distance;

        Up = new List<Point>(Distance.Select(c=>c.Clone()));
        Up.ForEach(p => p.X *= -1);

        Right = new List<Point>(Distance.Select(c => c.Clone()));
        Right.ForEach(p => p.Reverse());

        Left = new List<Point>(Right.Select(c => c.Clone()));
        Left.ForEach(p => p.Y *= -1);
    }

    public List<Point> GetLeftDirection()
    {
        return Distance;
    }
}
