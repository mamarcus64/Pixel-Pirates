using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideSweep : AimGame {
	Icon selector;
	Icon redZone;
	List<Icon> greenZones = new List<Icon>();
	float redZoneWidth = 2.25f, redZoneHeight = 1;
	float selectorSpeed;
	int direction = 1;

	public SideSweep Init(Weapon weapon, Entity target) {
		base.Init(weapon, target);
		selectorSpeed = 1.5f + Random.value * 2;
		selector = grayScreen.GetObject().AddComponent<Icon>().Init(SpritePath.blueSelector, new Vector2(redZoneWidth / 8, redZoneHeight + 0.25f),
			new Vector2(0, 1) + weaponPosition, "Aim Games.3", grayScreen);
		redZone = grayScreen.GetObject().AddComponent<Icon>().Init(SpritePath.redBar, new Vector2(redZoneWidth, redZoneHeight),
			new Vector2(0, 1) + weaponPosition, "Aim Games.1", grayScreen);
		greenZones.Add(grayScreen.GetObject().AddComponent<Icon>().Init(SpritePath.greenBar, new Vector2(redZoneWidth / 6, redZoneHeight),
			new Vector2(0, redZoneHeight) + weaponPosition, "Aim Games.2", grayScreen));
		return this;
	}

	void Update() {
		if (selector.GetRelativePosition().x >= redZoneWidth / 2 + redZone.GetRelativePosition().x)
			direction = -1;
		if (selector.GetRelativePosition().x <= -redZoneWidth / 2 + redZone.GetRelativePosition().x)
			direction = 1;
		selector.Move(new Vector2(selectorSpeed * direction * Time.deltaTime, 0));

		if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
			for (int i = 0; i < greenZones.Count; i++)
				if (Vector2.Distance(selector.GetRelativePosition(), greenZones[i].GetRelativePosition())
					< greenZones[i].GetWidth() / 1.9f)
					//not exactly divided by 2 to give the player some leeway in terms of hitboxes
					Finish(1);
			Finish(0);
		}

	}
}
