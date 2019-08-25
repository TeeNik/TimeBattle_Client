using System.Collections.Generic;

public abstract class GameSystem<T> : SystemBase<T> where T : ComponentBase
{
    protected List<int> ToDelete;

    protected GameSystem()
    {
        ToDelete = new List<int>();
    }

    public override void Update()
    {
        base.Update();

        foreach (var key in ToDelete)
        {
            Components.Remove(key);
        }
        ToDelete.Clear();
    }

    public abstract bool IsProcessing();

}
