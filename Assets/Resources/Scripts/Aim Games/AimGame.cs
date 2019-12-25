using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AimGame : MonoBehaviour {
	protected Icon grayScreen;
	protected Weapon weapon;
	protected Vector2 weaponPosition;
	protected Entity target;

	public AimGame Init(Weapon weapon, Entity target) {
		this.weapon = weapon;
		this.target = target;
		weaponPosition = weapon.GetAbsolutePosition();
		grayScreen = weapon.GetObject().AddComponent<Icon>().Init(SpritePath.grayBar, Camera.main.ScreenToWorldPoint
			(new Vector2(Screen.width, Screen.height) * 2),
			new Vector2(-weaponPosition.x, -weaponPosition.y), "Aim Games", weapon);
		grayScreen.SetOpacity(0.5f);
		return this;
	}

	public void Finish(float result) {
		weapon.Fire(target, result);
		grayScreen.Die();
		Destroy(this);
	}
}
