using System.Collections.Generic;

public class GameBalance
{
    public readonly MapConfig MapConfig;

    public GameBalance()
    {
        MapConfig = Utils.ParseConfig<MapConfig>("map");
    }

    public List<Point> GetCoversPoint()
    {
        var list = new List<Point>();
        var map = MapConfig.map;
        for (var i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == (int)OnMapType.Cover)
                {
                    list.Add(new Point(i,j));
                }
            }
        }
        return list;
    }

    public FlagQuestConfig GetFlagQuestConfig()
    {
        return Utils.ParseConfig<FlagQuestConfig>("flag_area");
    }
}
