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

    void Start()
    {
        spritePath = "Sprites/Projectiles/demo cannonball";
        width = 1f;
        height = 1f;
        speed = 8f;
        ProjectileStart();
    }

    void Update()
    {
        if (!ShipFightManager.paused)
            ProjectileUpdate();
    }
}
