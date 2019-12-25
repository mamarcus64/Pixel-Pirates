using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDemo : MonoBehaviour {
	// Start is called before the first frame update
	void Start() {
		//gameObject.AddComponent<PolygonCollider2D>();
	}

	// Update is called once per frame
	void Update() {
		float delta = 0.1f;
		//Debug.Log(Input.GetKey(KeyCode.W));
		if (Input.GetKey(KeyCode.W))
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + delta, -8);
		if (Input.GetKey(KeyCode.A))
			gameObject.transform.position = new Vector3(gameObject.transform.position.x - delta, gameObject.transform.position.y, -8);
		if (Input.GetKey(KeyCode.S))
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + -delta, -8);
		if (Input.GetKey(KeyCode.D))
			gameObject.transform.position = new Vector3(gameObject.transform.position.x + delta, gameObject.transform.position.y, -8);
	}

	public void OnCollisionEnter(Collision collision) {
		Debug.Log("Collide!");
	}
}
