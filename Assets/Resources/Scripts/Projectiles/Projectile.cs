using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : Entity {
	protected Entity target;
	protected Ship shooter;
	protected float speed;
	protected float epsilon = 0.1f;
	protected Vector2 direction;
	public Projectile Init(string spritePath, Vector2 size, Vector2 location, Entity target, Ship shooter) {
		base.Init(spritePath, size, location, "Projectiles");
		direction = new Vector2(1, 0);
		SetTarget(target);
		SetShooter(shooter);
		return this;
	}

	public void ProjectileUpdate() {
		EntityUpdate();
		Move(direction.normalized.x * speed * Time.deltaTime, direction.normalized.y * speed * Time.deltaTime);
		if (target != null)
			if (new Vector2(obj.transform.position.x - target.GetObject().transform.position.x,
				obj.transform.position.y - target.GetObject().transform.position.y).magnitude <= epsilon)
				ContactTarget();
	}

	public void ContactTarget() {
		HitEffects();
		Die();
	}

	public abstract void HitEffects();
	public void SetTarget(Entity entity) {
		target = entity;
		direction = new Vector2(-obj.transform.position.x + target.GetObject().transform.position.x,
			-obj.transform.position.y + target.GetObject().transform.position.y);
	}

	public void SetShooter(Ship ship) {
		shooter = ship;
	}

	public Ship GetShooter() {
		return shooter;
	}
}
