using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Entity
{
    protected int rechargeTime = 3;
    protected float recharge = 0;

    public Shield Init(Ship ship)
    {
        base.Init(SpritePath.demoShield, new Vector2(ship.GetWidth() + 4, ship.GetHeight() + 3.5f), new Vector2(0, 0), "Shields", 1, ship);
        return this;
    }

    void Update()
    {
        if (!ShipFightManager.paused)
        {
            EntityUpdate();
            if (health <= 0)
            {
                mRenderer.enabled = false;
                recharge += Time.deltaTime;
            }
            else
                mRenderer.enabled = true;
            if (recharge > rechargeTime)
            {
                health++;
                recharge = 0;
            }
        }
    }

    public override void GrayScale()
    {
        
    }

    public override void EndGrayScale()
    {
        
    }

    public override void SetGrayScale(bool set)
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Entity collided = collision.gameObject.GetComponent<EntityProxy>().GetEntity();
        if(mRenderer.enabled && collided is Projectile projectile && projectile.GetShooter() != GetParent())
            if(projectile.GetShooter() != this.GetParent())
            {
                projectile.Die();
                health--;
            }
    }
}
