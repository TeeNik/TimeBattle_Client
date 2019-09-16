using System.Collections.Generic;
using System.Linq;

public enum WeaponType
{
    Pistol,
    Assault,
    Shotgun,
    Sniper
}

public abstract class Weapon
{
    public readonly WeaponType Type;

    protected abstract List<Point> Distance { get; }
    public virtual List<Point> Left {get; protected set;}
    public virtual List<Point> Right {get; protected set;}
    public virtual List<Point> Up  {get; protected set;}
    public virtual List<Point> Down  { get; protected set; }

    protected Weapon(WeaponType type)
    {
        Type = type;
    }

    protected void GenerateBasicDirections()
    {
        Down = Distance;

        Up = new List<Point>(Distance.Select(c => c.Clone()));
        Up.ForEach(p => p.X *= -1);

        Right = new List<Point>(Distance.Select(c => c.Clone()));
        Right.ForEach(p => p.Reverse());

        Left = new List<Point>(Right.Select(c => c.Clone()));
        Left.ForEach(p => p.Y *= -1);
    }
}
