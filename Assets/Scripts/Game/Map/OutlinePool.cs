using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlinePool
{

    private Transform _parent;
    private List<GameObject> _pool;
    private const int Capacity = 50;

    public OutlinePool(Transform parent)
    {
        _pool = new List<GameObject>();
        _parent = parent;

        Generate(Capacity);
    }

    private void Generate(int amount)
    {
        var outlineBase = ResourceManager.Instance.Outline;
        for (int i = 0; i < amount; i++)
        {
            var obj = GameObject.Instantiate(outlineBase, _parent);
            obj.SetActive(false);
            _pool.Add(obj);
        }
    }

    public GameObject GetFromPool()
    {
        var obj = _pool.Find(g => !g.activeSelf);
        if (obj != null)
        {
            obj.SetActive(true);
            return obj;
        }
        Generate(Capacity/2);
        return GetFromPool();
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void ReturnAll()
    {
        foreach (var o in _pool)
        {
            o.SetActive(false);
        }
    }
}
