using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityManager<E>
{
    protected Entity owner;
    protected int maxSize;
    protected List<E> entities = new List<E>();
    public EntityManager(Entity owner)
    {
        this.owner = owner;
        this.maxSize = 10000; //arbitrary number where the manager practically doesn't have a max size
    }

    public EntityManager(Entity owner, int maxSize)
    {
        this.owner = owner;
        this.maxSize = maxSize;
    }

    public E Get(int i)
    {
        return entities[i];
    }

    public List<E> GetAll()
    {
        return entities;
    }

    public E Set(int i, E entity)
    {
        E oldEntity = entities[i];
        entities[i] = entity;
        return oldEntity;
    }

    public bool Add(E entity)
    {
        if (entities.Count >= maxSize)
            return false;
        entities.Add(entity);
        return true;
    }

    public E Remove(int i)
    {
        E oldEntity = entities[i];
        entities.RemoveAt(i);
        return oldEntity;
    }

    public void Place(Vector2 location, int i)
    {
        (entities[i] as Entity).SetParent(owner);
        (entities[i] as Entity).SetLocation(location);
    }

    public void Place(List<Vector2> locations)
    {
        for(int i = 0; i < locations.Count && i < entities.Count; i++)
        {
            (entities[i] as Entity).SetParent(owner);
            (entities[i] as Entity).SetLocation(locations[i]);
        }
    }

    public int GetMaxSize()
    {
        return maxSize;
    }

    public int Size()
    {
        return entities.Count;
    }
}
