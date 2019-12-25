using UnityEngine;

public class Icon : Entity {
	public Icon Init(string pathName, Vector2 size, Vector2 relativePosition, string layer, Entity parent) {
		return Init(pathName, size, relativePosition, layer, parent, true);
	}

	public Icon Init(string pathName, Vector2 size, Vector2 relativePosition, string layer, Entity parent, bool wantsCollider) {
		base.Init(pathName, size, relativePosition, layer, 0, parent, wantsCollider);
		return this;
	}

	void Update() {
		if (!ShipFightManager.paused) {
			EntityUpdate();
		}
	}
}
