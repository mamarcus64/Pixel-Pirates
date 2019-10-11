using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoShip : Ship
{

    void Start()
    {
        health = 6;
        spritePath = "Sprites/Ships/demo ship";
        width = 15;
        height = 2;
        roomPositions.Add(new Vector3(-1, -0.75f, 3));
        roomPositions.Add(new Vector3(-1, 0.75f, 3));
        roomPositions.Add(new Vector3(-1, 0, 1));
        roomPositions.Add(new Vector3(0, 0, 1));
        roomPositions.Add(new Vector3(-0.25f, -0.75f, 2));
        roomPositions.Add(new Vector3(-0.25f, 0.75f, 2));
        roomPositions.Add(new Vector3(0.25f, -0.75f, 2));
        roomPositions.Add(new Vector3(0.25f, 0.75f, 2));
        ShipStart();
        weaponManager.Add(obj.AddComponent<DemoWeapon>());
        weaponManager.Add(obj.AddComponent<DemoWeapon>());
        weaponPositions.Add(new Vector2(-1, 1.25f));
        weaponPositions.Add(new Vector2(-1, -1.25f));
        weaponPositions.Add(new Vector2(3.5f, 2.5f));
        weaponPositions.Add(new Vector2(3.5f, -3));      
    }

    void Update()
    {
        if (!ShipFightManager.paused)
            ShipUpdate();
    }
}
