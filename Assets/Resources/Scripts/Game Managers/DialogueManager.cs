using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour {
	GUIStyle justText;
    GameObject textObject;

	void Start() {
        GameObject myCanvas = new GameObject();
        Canvas canv = myCanvas.AddComponent<Canvas>();
        canv.renderMode = RenderMode.ScreenSpaceOverlay;
        textObject = new GameObject("Text Object");
        textObject.transform.SetParent(canv.transform);
        RectTransform trans = textObject.AddComponent<RectTransform>();
        trans.anchoredPosition = new Vector2(0, 0);
        trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);
        trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);
        Text text = textObject.AddComponent<Text>();
        text.text = "Goo goo ga ga ga eeilesjilin;n \n \n \n hoheoehfojkljelfh";
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        // transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 50);
        //transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50);
        //transform.sizeDelta = new Vector2(100, 100);
        //myCanvas.transform.position = new Vector3(0, 0, -1);
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -50);
		justText = new GUIStyle();
		justText.wordWrap = true;
		//Icon bg = gameObject.AddComponent<Icon>().Init(SpritePath.funnyImage, Camera.main.ScreenToWorldPoint
		//	(new Vector2(Screen.width, Screen.height)) * 2, new Vector2(0, 0), "Background", null);
		//Icon textBox = gameObject.AddComponent<Icon>().Init(SpritePath.woodTextbox, Camera.main.ScreenToWorldPoint
		//	(new Vector2(Screen.width, Screen.height)), new Vector2(0, 0), "Textbox", null);
		//bg.SetOpacity(0.4f);
        
		//justText.normal.background = null;
	}

    // Update is called once per frame
    int x = 50;
    public void Update() {
        Vector3 currentPos = textObject.GetComponent<RectTransform>().anchoredPosition;
        if (Input.GetKey(KeyCode.Space)) {
            x++;
            textObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(currentPos.x + 1, currentPos.y, currentPos.z);
            textObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x);
        }
        Debug.Log(Input.mousePosition);
    }
    public Rect WorldToPixel(Vector2 pos, Vector2 size) {
		Vector2 pixelPos = Camera.main.WorldToScreenPoint(pos);
		Vector2 pixelSize = Camera.main.WorldToScreenPoint(size) - Camera.main.WorldToScreenPoint(new Vector2(0, 0));
		Rect rect = new Rect(pixelPos.x - pixelSize.x / 2, pixelPos.y - pixelSize.y / 2, pixelSize.x, pixelSize.y);
		return rect;
	}
}
