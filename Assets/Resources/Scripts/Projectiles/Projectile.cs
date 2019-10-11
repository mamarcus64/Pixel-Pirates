using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : Entity
{
    protected Entity target;
    protected Ship shooter;
    protected float speed;
    protected float epsilon = 0.1f;
    protected Vector2 direction;
   public void ProjectileStart()
    {
        EntityStart();
        wantsFocus = false;
        obj.GetComponent<SpriteRenderer>().sortingLayerName = "Projectiles";
        direction = new Vector2(1, 0);
    }

    public void ProjectileUpdate()
    {
        EntityUpdate();
        SetLocation(localPosition.x + direction.normalized.x * speed * Time.deltaTime,
            localPosition.y + direction.normalized.y * speed * Time.deltaTime);
        if(target != null)
            if(new Vector2(obj.transform.position.x - target.GetObject().transform.position.x, 
                obj.transform.position.y - target.GetObject().transform.position.y).magnitude <= epsilon)
               ContactTarget();
    }

    public void ContactTarget()
    {
        HitEffects();
        Die();
    }

    public abstract void HitEffects();
    public void SetTarget(Entity entity)
    {
        target = entity;
        direction = new Vector2(-obj.transform.position.x + target.GetObject().transform.position.x, 
            -obj.transform.position.y + target.GetObject().transform.position.y);
    }

    public void SetShooter(Ship ship)
    {
        shooter = ship;
    }

    public Ship GetShooter()
    {
        return shooter;
    }
}
