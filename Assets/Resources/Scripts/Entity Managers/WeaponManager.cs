using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : EntityManager<Weapon> {
	public WeaponManager(Entity owner) : base(owner, 4) {

	}
}
