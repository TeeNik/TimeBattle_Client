public class Point
{
    public int X;
    public int Y;

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Point Clone()
    {
        return new Point(X, Y);
    }

    public bool Equals(Point other)
    {
        return X == other.X && Y == other.Y;
    }

    public void Reverse()
    {
        var temp = X;
        X = Y;
        Y = temp;
    }

    public Point Sum(Point p)
    {
        return new Point(X + p.X, Y + p.Y);
    }

    public Point Substract(Point p)
    {
        return new Point(X - p.X, Y - p.Y);
    }

}
