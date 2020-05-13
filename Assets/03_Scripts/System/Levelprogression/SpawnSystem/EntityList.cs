using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityList<T> : ScriptableObject
{
    public List<T> entityList;

    public void Add(T entity)
    {
        entityList.Add(entity);
    }

    public void Remove(T entity)
    {
        if (entityList.Contains(entity))
            entityList.Remove(entity);
    }
}
