using UnityEngine;
using UnityEngine.SceneManagement;
public class DialogueManager : MonoBehaviour
{
    float width = 5;
    float height = 5;
    GUIStyle justText;
    void Start()
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -50);
        justText = new GUIStyle();
        Icon bg = gameObject.AddComponent<Icon>().Init(SpritePath.funnyImage, Camera.main.ScreenToWorldPoint
            (new Vector2(Screen.width, Screen.height) * 2), new Vector2(0, 0), "", null);
        bg.SetOpacity(0.2f);
        //justText.normal.background = null;
    }

    // Update is called once per frame
    

    void OnGUI()
    {
        // Make a background box
        GUI.skin.button.wordWrap = true;
        GUI.Label(new Rect(50, 10, 100, 500), "Loader Menu menu loader loader menu", justText);
        //GUI.Box(new Rect(10, 10, 100, 90), "Loader Menu menu loader loader menu");

        // Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
        if (GUI.Button(new Rect(20, 40, 80, 20), "Level 1", justText))
        {
            SceneManager.LoadSceneAsync("ShipFight");
        }

        // Make the second button.
        if (GUI.Button(new Rect(20, 70, 80, 20), "Level 2"))
        {
            Debug.Log("b2");
        }
    }
}
