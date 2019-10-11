using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Entity
{
    protected Ship mShip;
    protected int rechargeTime = 3;
    protected float recharge = 0;

    public void SetShip(Ship newShip)
    {
        mShip = newShip;
    }

    void Start()
    {
        width = 15;
        height = 5;
        health = 1;
        layer = "Shields";
        spritePath = "Sprites/Shields/demo shield";
        EntityStart();
        StartCoroutine(Load());
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

    public IEnumerator Load()
    {

        yield return new WaitForSeconds(Entity.bufferTime);
        this.SetParent(mShip);
        if (obj != null)
            localPosition = obj.transform.localPosition;
        SetLocation(0, 0);
        Resize(mShip.getWidth() + 4, mShip.GetHeight() + 3.5f);
        //Destroy(obj.GetComponent<PolygonCollider2D>());
       // mCollider = null;
        //ellipseCollider = obj.AddComponent<EllipseCollider2D>();
        //ellipseCollider.radiusX = (mShip.getWidth() + 4) / 1;
        //ellipseCollider.radiusY = (mShip.GetHeight()) / 1;
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
        if(mRenderer.enabled && collided is Projectile projectile && projectile.GetShooter() != this.mShip)
            if(projectile.GetShooter() != this.getShip())
            {
                projectile.Die();
                health--;
            }
    }

    public Ship getShip()
    {
        return mShip;
    }

    

}
