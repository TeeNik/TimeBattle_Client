using System.Collections.Generic;

public class ActionConfigData
{
    public string name;
    public int energy;
    public string icon;
    public ActionType type;
}

public class ActionConfig
{
    public List<ActionConfigData> actions;
}
