using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Player {
	private Dictionary<Weapon, float> weaponFireTimes = new Dictionary<Weapon, float>();

	public override void Play(Ship playerShip, Ship enemyShip) {
		if (playerShip == null) {
			return;
		}

		foreach (Weapon weapon in enemyShip.GetWeapons()) {
			if (!weapon.WeaponLoaded()) {
				continue;
			}

			if (!weaponFireTimes.ContainsKey(weapon)) {
				weaponFireTimes.Add(weapon, Time.time + Random.Range(0.4f, 0.8f));
			} else {
				if (Time.time > weaponFireTimes[weapon]) {
					weapon.Fire(playerShip.GetRooms()[Random.Range(0, playerShip.GetRooms().Count)], 1);
					weaponFireTimes.Remove(weapon);
				}
			}
		}
	}
}
