using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityManager
{
    protected Entity owner;
    protected int maxSize;
    protected List<Entity> entities = new List<Entity>();
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

    public Entity Get(int i)
    {
        return entities[i];
    }

    public List<Entity> GetAll()
    {
        return entities;
    }

    public Entity Set(int i, Entity entity)
    {
        Entity oldEntity = entities[i];
        entities[i] = entity;
        return oldEntity;
    }

    public bool Add(Entity entity)
    {
        if (entities.Count >= maxSize)
            return false;
        entities.Add(entity);
        return true;
    }

    public Entity Remove(int i)
    {
        Entity oldEntity = entities[i];
        entities.RemoveAt(i);
        return oldEntity;
    }

    public void Place(Vector2 location, int i)
    {
        entities[i].SetParent(owner);
        entities[i].SetLocation(location);
    }

    public void Place(List<Vector2> locations)
    {
        for(int i = 0; i < locations.Count && i < entities.Count; i++)
        {
            entities[i].SetParent(owner);
            entities[i].SetLocation(locations[i]);
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
