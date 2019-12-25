using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballMkI : Projectile {
	public override void HitEffects() {
		target.TakeDamage(1);
	}

	public override void OnFocusLost(Entity entity) {

	}

	public CannonballMkI Init(Vector2 location, Entity target, Ship shooter) {
		base.Init(SpritePath.demoCannonball, new Vector2(1, 1), location, target, shooter);
		speed = 8f;
		return this;
	}

	void Update() {
		if (!ShipFightManager.paused)
			ProjectileUpdate();
	}
}
