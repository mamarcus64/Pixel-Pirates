using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : Projectile {
	private int damage;

	public override void HitEffects() {
		target.TakeDamage(damage);
	}

	public override void OnFocusLost(Entity entity) {

	}

	public BasicProjectile Init(string spritePath, Vector2 size, int damage, Vector2 location, Entity target, Ship shooter) {
		base.Init(spritePath, size, location, target, shooter);
		this.damage = damage;
		this.speed = 8f;
		return this;
	}

	void Update() {
		if (!GameManager.paused) {
			ProjectileUpdate();
		}
	}

    public override string GetDisplayName() {
        return "Basic Projectile";
    }
}
