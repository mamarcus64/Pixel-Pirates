using UnityEngine;
using UnityEngine.SceneManagement;
public class DialogueManager : MonoBehaviour {
	GUIStyle justText;
	void Start() {
		Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -50);
		justText = new GUIStyle();
		justText.wordWrap = true;
		Icon bg = gameObject.AddComponent<Icon>().Init(SpritePath.funnyImage, Camera.main.ScreenToWorldPoint
			(new Vector2(Screen.width, Screen.height)) * 2, new Vector2(0, 0), "Background", null);
		Icon textBox = gameObject.AddComponent<Icon>().Init(SpritePath.woodTextbox, Camera.main.ScreenToWorldPoint
			(new Vector2(Screen.width, Screen.height)), new Vector2(0, 0), "Textbox", null);
		bg.SetOpacity(0.4f);
		//justText.normal.background = null;
	}

	// Update is called once per frame
	public Rect WorldToPixel(Vector2 pos, Vector2 size) {
		Vector2 pixelPos = Camera.main.WorldToScreenPoint(pos);
		Vector2 pixelSize = Camera.main.WorldToScreenPoint(size) - Camera.main.WorldToScreenPoint(new Vector2(0, 0));
		Rect rect = new Rect(pixelPos.x - pixelSize.x / 2, pixelPos.y - pixelSize.y / 2, pixelSize.x, pixelSize.y);
		return rect;
	}

	void OnGUI() {
		GUI.Button(WorldToPixel(new Vector2(0, 0), Camera.main.ScreenToWorldPoint
			(new Vector2(Screen.width, Screen.height)) * 0.9f), "We love monky \n monky monky monky \n \n \n yes yes", justText);
		// if (GUI.Button(WorldToPixel(new Vector2(-2f, 1), new Vector2(0.75f, 0.375f)), "Yes we do"))
		if (GUI.Button(new Rect(300, 300, 50, 50), "Yes we do")) {
			SceneManager.LoadSceneAsync("ShipFight");
		}

		// Make the second button.
		if (GUI.Button(new Rect(20, 70, 80, 20), "No")) {
			SceneManager.LoadSceneAsync("ShipFight");
		}
	}
}
