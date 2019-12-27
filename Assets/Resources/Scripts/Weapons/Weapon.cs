using System.Collections;
using UnityEngine;

public abstract class Weapon : Entity {
	protected float cooldown;
	protected float cooldownTimer = 0;
	protected Icon greenBar;
	protected Icon redBar;
	public static float cooldownWidth = 0.625f;
	public static float cooldownHeight = 0.25f;

	public Weapon Init(string spritePath, Vector2 size, Vector2 location, int health, Entity parent) {
		base.Init(spritePath, size, location, "Weapons", health, parent);
		wantsFocus = true;
		Vector2 barLoc = new Vector2(0, 1); //relative to parent, which in for green/red bar's case is Weapon
		greenBar = obj.AddComponent<Icon>().Init(SpritePath.greenBar,
			new Vector2(1f, cooldownHeight), barLoc, "Green Bar", this);
		redBar = obj.AddComponent<Icon>().Init(SpritePath.redBar,
			new Vector2(cooldownWidth, cooldownHeight), barLoc, "Red Bar", this);
		return this;
	}

	public void WeaponUpdate() {
		EntityUpdate();
		if (!WeaponLoaded()) {
			cooldownTimer += Time.deltaTime;
		}
		DrawCooldownBar();
	}

	public bool WeaponLoaded() {
		return cooldownTimer >= cooldown;
	}

	public void DrawCooldownBar() {
		greenBar.Resize(cooldownWidth * (cooldownTimer / cooldown), cooldownHeight);
		greenBar.SetLocation(redBar.GetRelativePosition().x - cooldownWidth / 2
		 + (cooldownWidth * (cooldownTimer / cooldown) / 2), redBar.GetRelativePosition().y);
		//redBar.SetLocation
		//greenBar.transform.position = new Vector3( greenBar.transform.position.z);
	}

	public override void TakeDamage(int damage) {
		health -= damage;
		SetGrayScale(health <= 0);
	}

	public override void OnFocusLost(Entity to) {
		if (to == null || !WeaponLoaded()) {
			return;
		}

		if (to.GetType().IsSubclassOf(typeof(CrewMember))) {
			to = (to as CrewMember).GetRoom(); //if clicking on enemy crew member on opponent ship (still want to fire at respective room)
		}

		if (to.GetType().IsSubclassOf(typeof(Room)) && ShipFightManager.GetEnemyShip().GetRooms().Contains(to as Room)) {
			cooldownTimer = 0;
			ShipFightManager.paused = true;
			UserGame(to);
		}
	}

	public abstract void Fire(Entity target, float result);

	public abstract void UserGame(Entity to);
}
