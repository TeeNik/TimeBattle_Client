public enum OnMapType
{
    Wall,
    Empty,
    Player1,
    Player2,
    Cover
}


public struct MapData{

    public OnMapType Type;
    public int? EntityId;

}
