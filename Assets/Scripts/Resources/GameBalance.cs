using System.Collections.Generic;

public class GameBalance
{
    public readonly int[][] Map;

    public GameBalance()
    {
        var obj = Utils.ParseConfig<MapConfig>("map");
        Map = obj.map;
    }

    public List<Point> GetCoversPoint()
    {
        var list = new List<Point>();
        for (var i = 0; i < Map.Length; i++)
        {
            for (int j = 0; j < Map[i].Length; j++)
            {
                if (Map[i][j] == (int)OnMapType.Cover)
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
