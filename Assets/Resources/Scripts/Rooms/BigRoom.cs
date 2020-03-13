using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRoom : Room {
	public override void OnFocusLost(Entity entity) {

	}

	// Start is called before the first frame update
	public BigRoom Init(Vector2 location, Entity parent) {
		base.Init(SpritePath.demoRoom, location, parent);
		return this;
	}

	// Update is called once per frame
	void Update() {
		if (!GameManager.paused)
			RoomUpdate();
	}

    public override string GetDisplayName() {
        return "Big Room";
    }

}
