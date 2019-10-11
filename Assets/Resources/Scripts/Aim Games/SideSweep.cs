using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideSweep : AimGame
{
    
    GameObject selector;
    GameObject redZone;
    List<GameObject> greenZones = new List<GameObject>();
    float redZoneWidth = 2.25f, redZoneHeight = 1;
    float selectorSpeed;
    int direction = 1;
    void Start()
    {
        AimGameStart();
        selectorSpeed = 0.5f + Random.value * 2;
        selector = new GameObject("Selector");
        redZone = new GameObject("Red Zone");
        greenZones.Add(new GameObject("Green Zone"));
        StartCoroutine(LoadZones());
    }

    IEnumerator LoadZones()
    {
        yield return new WaitForSeconds(0.0001f);
        selector.transform.parent = parent.transform;
        selector.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Misc/blue selector");
        selector.transform.localPosition = new Vector3(-redZoneWidth / 2, redZoneHeight, -0.03f);
        Resize(redZoneWidth / 8, redZoneHeight + 0.25f, selector);
        redZone.transform.parent = parent.transform;
        redZone.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Misc/red bar");
        redZone.transform.localPosition = new Vector3(0, redZoneHeight, -0.01f);
        Resize(redZoneWidth, redZoneHeight, redZone);
        greenZones[0].transform.parent = parent.transform;
        greenZones[0].AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Misc/green bar");
        greenZones[0].transform.localPosition = new Vector3(0, redZoneHeight, -0.02f);
        Resize(redZoneWidth / 6, redZoneHeight, greenZones[0]);
    }

    void Update()
    {
        if (selector.transform.localPosition.x >= redZoneWidth / 2)
            direction = -1;
        if (selector.transform.localPosition.x <= -redZoneWidth / 2)
            direction = 1;
        selector.transform.localPosition = new Vector3(selector.transform.localPosition.x + selectorSpeed * direction * Time.deltaTime,
            selector.transform.localPosition.y, selector.transform.localPosition.z);

        if (Input.GetKeyDown(KeyCode.Space)) {
            for (int i = 0; i < greenZones.Count; i++)
                if (Vector2.Distance(selector.transform.position, greenZones[i].transform.position)
                    < greenZones[i].GetComponent<SpriteRenderer>().sprite.bounds.size.x / 1.8f * greenZones[i].transform.localScale.x)
                    //not exactly divided by 2 to give the player some leeway in terms of hitboxes
                    Finish(1);
            Finish(0);
        }
    }
}
