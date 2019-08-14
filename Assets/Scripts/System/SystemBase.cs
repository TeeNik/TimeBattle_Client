using System.Collections.Generic;

public abstract class SystemBase<T> where T : ComponentBase
{

    protected Dictionary<int, T> Components;

    protected SystemBase()
    {
        Components = new Dictionary<int, T>();
    }

    public abstract void Update();

}
