using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballMkII : Projectile {
    public override void HitEffects() {
        target.TakeDamage(2);
    }

    public override void OnFocusLost(Entity entity) {

    }

    public CannonballMkII Init(Vector2 location, Entity target, Ship shooter) {
        base.Init(SpritePath.demoCannonball, new Vector2(1.5f, 1.5f), location, target, shooter);
        speed = 5f;
        return this;
    }

    void Update() {
        if (!ShipFightManager.paused)
            ProjectileUpdate();
    }
}
