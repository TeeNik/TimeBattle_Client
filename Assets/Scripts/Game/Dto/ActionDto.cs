﻿using System.Collections.Generic;

public class ActionDto
{
    public int entityId;
    public List<ActionPhase> phases;

}

public class ActionPhase
{
    public ActionType type;
    public ComponentBase component;
}