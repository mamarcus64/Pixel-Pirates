using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFightManager : MonoBehaviour {
	static Ship playerShip;
	static Ship enemyShip;
	public static bool paused;
	void Start() {
		paused = false;
		playerShip = gameObject.AddComponent<BasicShip>().Init(new Vector2(0, 2), new User());
		Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -50);
		enemyShip = gameObject.AddComponent<BasicShip>().Init(new Vector2(0, -2.5f), new Enemy());
		playerShip.SetPlayerOwned(true);
		//StartCoroutine(Load());
	}

	public static IEnumerator Pause(float time) {
		paused = true;
		yield return new WaitForSeconds(time);
		paused = false;
	}

	void Update() {
		if (enemyShip == null)
			Debug.Log("uh oh");
	}

	public static Ship GetEnemyShip() {
		return enemyShip;
	}

	public static Ship GetPlayerShip() {
		return playerShip;
	}

	public static void GrayScale() {
		playerShip.GrayScale();
		enemyShip.GrayScale();
	}

	public static void EndGrayScale() {
		playerShip.EndGrayScale();
		enemyShip.EndGrayScale();
	}

}
