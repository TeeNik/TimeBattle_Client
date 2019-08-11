using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager
{

    public Dictionary<int, Entity> Entities;

    public EntityManager()
    {
        Entities = new Dictionary<int, Entity>();
    }

    public Entity GetEntity(int id)
    {
        return Entities[id];
    }

}
