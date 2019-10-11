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
        //the way roomPositions currently works: the first two values are the x and y position relative to the center of the ship
        //the third value is essentially an enum that corresponds to the type of room; 0 = 1x1 (single) room, 1 = 2x1 (long) room, 
        //2 = 1x2 (tall) room and 3 = 2x2 (big) room; to be honest, because rooms are not going to change from copies of the same ship,
        //the room positions can just be manually entered in code every time for however many types of ships we will design
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
