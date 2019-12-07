using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideSweep : AimGame
{
    
    Icon selector;
    Icon redZone;
    List<GameObject> greenZones = new List<GameObject>();
    float redZoneWidth = 2.25f, redZoneHeight = 1;
    float selectorSpeed;
    int direction = 1;
    void Start()
    {
        AimGameStart();
        selectorSpeed = 0.5f + Random.value * 2;
        selector = grayScreen.GetObject().AddComponent<Icon>().Init("Sprites/Misc/blue selector", new Vector2(redZoneWidth / 8, redZoneHeight + 0.25f),
            new Vector2(4, -1.5f), "Aim Games.3", grayScreen);
        redZone = grayScreen.GetObject().AddComponent<Icon>().Init("Sprites/Misc/red bar", new Vector2(redZoneWidth, redZoneHeight),
            new Vector2(0.5f, 0), "Aim Games.1", grayScreen);
        greenZones.Add(new GameObject("Green Zone"));
        StartCoroutine(LoadZones());
    }

    IEnumerator LoadZones()
    {
        yield return new WaitForSeconds(0.0001f);
        /*
        selector.transform.parent = grayScreen.transform;
        selector.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Misc/blue selector");
        selector.transform.localPosition = new Vector3(-redZoneWidth / 2, redZoneHeight, -0.03f);
        Resize(redZoneWidth / 8, redZoneHeight + 0.25f, selector);
        redZone.transform.parent = grayScreen.transform;
        redZone.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Misc/red bar");
        redZone.transform.localPosition = new Vector3(0, redZoneHeight, -0.01f);
        Resize(redZoneWidth, redZoneHeight, redZone);
        */
        /*
        GameObject grandparent = new GameObject();
        grandparent.transform.localPosition = new Vector2(1, 1);
        Debug.Log("Absolute position: " + grandparent.transform.position + " Local position: " + grandparent.transform.localPosition);
        grandparent.transform.localScale = new Vector3(4, 45442, -3);
        Debug.Log("Absolute position: " + grandparent.transform.position + " Local position: " + grandparent.transform.localPosition);
        
        GameObject parent = new GameObject();
        parent.transform.parent = grandparent.transform;
        parent.transform.localPosition = new Vector2(2, 3);
        Debug.Log("Absolute position: " + parent.transform.position + " Local position: " + parent.transform.localPosition);
        GameObject child = new GameObject();
        child.transform.parent = parent.transform;
        child.transform.localPosition = new Vector2(4, 7);
        Debug.Log("Absolute position: " + child.transform.position + " Local position: " + child.transform.localPosition);
        */
        

        greenZones[0].transform.parent = grayScreen.transform;
        greenZones[0].AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Misc/green bar");
        greenZones[0].transform.localPosition = new Vector3(0, redZoneHeight, -0.2f);
        Resize(redZoneWidth / 6, redZoneHeight, greenZones[0]);
    }

    void Update()
    {
        Vector3 scale = grayScreen.GetObject().transform.localScale;
        grayScreen.GetObject().transform.localScale = new Vector3(scale.x * 1.01f, scale.y * 1.01f, scale.z);
        Debug.Log(grayScreen.GetAbsolutePosition() + " " + redZone.GetAbsolutePosition());
        if (selector.GetLocalPosition().x >= redZoneWidth / 2)
            direction = -1;
        if (selector.GetLocalPosition().x <= -redZoneWidth / 2)
            direction = 1;
        selector.Move(new Vector2(selectorSpeed * direction * Time.deltaTime, 0));
        if (Random.value < 0.03f)
            ;//.Log(selector.GetLocalPosition().ToString("F3"));

        if (Input.GetKeyDown(KeyCode.Space)) {
            /*
            for (int i = 0; i < greenZones.Count; i++)
                if (Vector2.Distance(selector.transform.position, greenZones[i].transform.position)
                    < greenZones[i].GetComponent<SpriteRenderer>().sprite.bounds.size.x / 1.9f * greenZones[i].transform.localScale.x)
                    //not exactly divided by 2 to give the player some leeway in terms of hitboxes
                    Finish(1);
                    */
            Finish(0);
        }
        
    }
}
