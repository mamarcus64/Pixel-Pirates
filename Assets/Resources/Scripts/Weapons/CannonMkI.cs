using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMkI : Weapon {
	public CannonMkI Init(Vector2 location, Entity parent) {
		Init(SpritePath.cannonMkI, new Vector2(1.5f, 1), location, 2, parent);
		cooldown = 2.5f;
		return this;
	}

	void Update() {
		if (!ShipFightManager.paused) {
			WeaponUpdate();
		}
	}

    public override void UserGame(Entity to) {
        obj.AddComponent<SideSweep>().Init(this, to); //SideSweep will fire at target if successful
    }

	public override void Fire(Entity target, float result) {
		ShipFightManager.paused = false;
		cooldownTimer = 0;
		if (result == 1) {
			obj.AddComponent<BasicProjectile>().Init(SpritePath.demoCannonball, new Vector2(1, 1), 1, new Vector2(obj.transform.position.x, obj.transform.position.y), target, parent as Ship);
			//using absolute position, not relative position for projectile location b/c ball is not tied to a parent ship
		}
	}
}
