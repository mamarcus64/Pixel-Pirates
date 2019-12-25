using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour {
	GameObject a;
	GameObject b;
	GameObject c;
	void Start() {
		a = new GameObject();
		b = new GameObject();
		c = new GameObject();
		b.transform.parent = c.transform;
		a.transform.parent = b.transform;
		SetLocal(new Vector2(-2.2f, -1.7f), c);
		SetLocal(new Vector2(2.5f, 3.25f), b);
		SetLocal(new Vector2(3, 4), a);
	}

	float lastTime;
	float period = 0.875f;
	void Update() {
		if (Time.time - lastTime >= period) {
			lastTime = Time.time;
			SetScale(GetScale(c) * -2f, c);
			SetScale(GetScale(b) * 1.1f, b);
			Debug.Log("a: " + PrintTheWorks(a));
			Debug.Log("b: " + PrintTheWorks(b));
			Debug.Log("c: " + PrintTheWorks(c));
		}
	}

	public string PrintTheWorks(GameObject a) {
		return ("global:" + GetGlobal(a).ToString("F2") + " local:" + GetLocal(a).ToString("F2") + " scale:" + GetScale(a).ToString("F2"));
	}

	public Vector3 GetLocal(GameObject obj) {
		return obj.transform.localPosition;
	}

	public void SetLocal(Vector3 loc, GameObject obj) {
		obj.transform.localPosition = loc;
	}

	public Vector3 GetGlobal(GameObject obj) {
		return obj.transform.position;
	}

	public void SetGlobal(Vector3 loc, GameObject obj) {
		obj.transform.position = loc;
	}

	public Vector3 GetScale(GameObject obj) {
		return obj.transform.localScale;
	}

	public void SetScale(Vector3 loc, GameObject obj) {
		obj.transform.localScale = loc;
	}
}
