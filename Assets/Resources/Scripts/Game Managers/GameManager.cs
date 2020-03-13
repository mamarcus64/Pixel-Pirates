using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static bool paused = false;
    private static GameObject canvas;

    void Awake() {
        canvas = new GameObject("Canvas");
        canvas.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static GameObject MakeText(string words, Vector2 pos, Vector2 size) {
        return MakeText(words, pos, size, TextAnchor.UpperLeft);
    }
    public static GameObject MakeText(string words, Vector2 pos, Vector2 size, TextAnchor alignment) {
        return MakeText(words, pos, size, alignment, 12, Color.black, "Fonts/Nikaia");
    }
    public static GameObject MakeText(string words, Vector2 pos, Vector2 size, TextAnchor alignment, int fontSize) {
        return MakeText(words, pos, size, alignment, fontSize, Color.black, "Fonts/Nikaia");
    }
        public static GameObject MakeText(string words, Vector2 pos, Vector2 size, TextAnchor alignment, int fontSize, Color color, string fontPath) {
        //pos and size passed in as global coordinates - pos references center of textbox, so BOUNDING SIZE MATTERS when determining where text goes in the textbox
        GameObject textObject = (words.Length >= 15 ? new GameObject(words.Substring(0, 14)) : new GameObject(words));
        textObject.transform.SetParent(canvas.transform);
        Text text = textObject.AddComponent<Text>();
        text.text = words;
        text.alignment = alignment;
        text.fontSize = fontSize;
        text.font = Resources.Load<Font>(fontPath);
        text.color = color;
        text.font.material.mainTexture.filterMode = FilterMode.Point;
        RectTransform textTransform = textObject.GetComponent<RectTransform>();
        Vector3 center = Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0));
        textTransform.anchoredPosition = Camera.main.WorldToScreenPoint(pos) - center;
        //pixel coordinates usually start origin is in bottom left corner and world units start from center of screen
        //for some reason, RectTransform uses pixel units but origin is at center of screen
        textTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Camera.main.WorldToScreenPoint(size).x - center.x);
        textTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Camera.main.WorldToScreenPoint(size).y - center.y);
        textTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 35);
        return textObject;
    }
}
