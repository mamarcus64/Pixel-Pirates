using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCrew : CrewMember {
	public BasicCrew Init(Ship ship, Room room) {
		base.Init(SpritePath.demoCrew, ship, room);
		return this;
	}

	void Update() {
		if (!ShipFightManager.paused) {
			CrewUpdate();
		}
	}
}
