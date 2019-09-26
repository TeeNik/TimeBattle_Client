using System.Collections.Generic;

public class ReachAreaData
{
    public PlayerType Player;
    public List<Point> Area;
}

public class FlagQuestConfig
{
    public Point FlagSpawn;
    public List<ReachAreaData> AreaData;
}
