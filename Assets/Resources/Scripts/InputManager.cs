using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
	static Entity focus;
	public static int pixelScale = 100;
	void Start() {

	}

	void Update() {
		if (!ShipFightManager.paused) {
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
		}
	}

	public static void GiveFocus(Entity entity) {
        Debug.Log("focus: " + focus + " entity: " + entity);
        if (focus != null) {
            Debug.Log("focus lost");
			focus.OnFocusLost(entity);
			focus.SetOutline(false);
			focus = null;
		}
		//if (focus == null && entity != null && entity.PlayerOwned() && entity.wantsFocus) {
        if (focus == null) {
            Debug.Log("no focus");
            if (entity != null) {
                Debug.Log("entity not null");
                if (true) {//entity.PlayerOwned()) {
                    Debug.Log("Played owned");
                    if (entity.wantsFocus) {
                        Debug.Log("Wants focus");
                        entity.OnFocusGained(focus);
                        focus = entity;
                        focus.SetOutline(true);
                    }
                }
            }
		}
	}
}
