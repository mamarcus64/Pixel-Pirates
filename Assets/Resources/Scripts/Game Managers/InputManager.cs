using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
	static Entity focus;
	public static int pixelScale = 100;

	void Start() {

	}

	void Update() {
			RaycastHit2D[] hits;
			Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			point.z = Camera.main.transform.position.z;
			hits = Physics2D.RaycastAll(point, Vector3.forward, 100);
			if (hits.Length != 0) {
				if (Input.GetMouseButtonDown(0)) {
					Entity hitEntity = hits[0].collider.gameObject.GetComponent<EntityProxy>().GetEntity();
					InputManager.GiveFocus(hitEntity);
					if (DebugToggler.inputClick)
						Debug.Log(hitEntity.GetType().Name + " pressed at absolute position: "
							+ hitEntity.GetAbsolutePosition().ToString("F2") + " and local position: "
							+ hitEntity.GetRelativePosition().ToString("F2") + " Parent: "
							+ (hitEntity.GetParent() == null ? " None " : hitEntity.GetParent().GetType().Name)
							+ " Pixel: " + Input.mousePosition);
				}
			} else if (Input.GetMouseButton(0)) {
				GiveFocus(null);
			}

		if (Input.GetKeyDown(KeyCode.Space)) {
			GameManager.paused = ShipFightManager.userPaused = !ShipFightManager.userPaused;
		}
	}

	public static void GiveFocus(Entity entity) {
        if (focus != null) {
			focus.OnFocusLost(entity);
			focus.SetOutline(false);
			focus = null;
		}
		if (focus == null && entity != null && entity.playerOwned && entity.wantsFocus) {
            focus = entity;
            focus.OnFocusGained();
            focus.SetOutline(true);
		}
	}
}
