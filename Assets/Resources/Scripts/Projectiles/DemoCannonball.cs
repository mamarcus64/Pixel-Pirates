using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoCannonball : Projectile
{
    public override void HitEffects()
    {
        target.TakeDamage(1);
    }

    public override void OnFocusClick(Entity entity)
    {
        
    }

    public DemoCannonball Init(Vector2 location, Entity target, Ship shooter)
    {
        base.Init("Sprites/Projectiles/demo cannonball", new Vector2(1, 1), location, target, shooter);
        speed = 8f;
        return this;
    }

    void Update()
    {
        if (!ShipFightManager.paused)
            ProjectileUpdate();
    }
}
