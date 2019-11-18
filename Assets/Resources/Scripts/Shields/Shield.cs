using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Entity
{
    protected Ship ship;
    protected int rechargeTime = 3;
    protected float recharge = 0;

    public void SetShip(Ship newShip)
    {
        ship = newShip;
        SetParent(ship);
    }

    public Shield Init(Ship ship)
    {
        base.Init("Sprites/Shields/demo shield", new Vector2(15, 5), new Vector2(0, 0), "Shields");
        SetShip(ship);
        if (obj != null)
            localPosition = obj.transform.localPosition;
        SetLocation(0, 0, -GetZPosition("Ships") + GetZPosition("Shields"));//need to assign again bc now it has the ship as a parent
        //need to do the funky z-position stuff bc shield technically hasn't loaded in yet w/ correct z-value
        Resize(ship.GetWidth() + 4, ship.GetHeight() + 3.5f);
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
        if(mRenderer.enabled && collided is Projectile projectile && projectile.GetShooter() != this.ship)
            if(projectile.GetShooter() != this.GetShip())
            {
                projectile.Die();
                health--;
            }
    }

    public Ship GetShip()
    {
        return ship;
    }

    

}
