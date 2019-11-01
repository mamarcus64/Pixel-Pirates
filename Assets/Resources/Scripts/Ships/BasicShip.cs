using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShip : Ship {
	void Start() {
		health = 6;
		spritePath = "Sprites/Ships/basic ship";
		width = 1280 / 100;
		height = 432 / 100;
        /*
         the way roomPositions currently works: the first two values are the x and y position relative to the center of the ship
        the third value is essentially an enum that corresponds to the type of room; 0 = 1x1 (single) room, 1 = 2x1 (long) room, 
        2 = 1x2 (tall) room and 3 = 2x2 (big) room; to be honest, because rooms are not going to change from copies of the same ship,
        the room positions can just be manually entered in code every time for however many types of ships we will design

        With that being said, the way rooms are generated are with and x and y corresponding to the relative location of the topleft most cell in the room
        i.e. (0, 0, 2) would be a tall room with the top cell of the room (since there is no topleft cell) located at (0, 0) relative to the rest of the cells
        and (1, 0, 2) would be a tall room directly to the right of it, since the x value is one greater
        add an offset after ShipStart to move EVERY room in that direction
        */
        
        roomPositions.Add(new Vector3(-1, 0, 1));
        roomPositions.Add(new Vector3(-1, 1, 1));
        roomPositions.Add(new Vector3(-1, 3, 2));
        roomPositions.Add(new Vector3(0, 3, 2));
        //roomPositions.Add(new Vector3(-1, 0, 1));
        ShipStart();
        roomManager.SetOffset(new Vector2(0, -0.5f));

        weaponManager.Add(obj.AddComponent<DemoWeapon>());
		weaponManager.Add(obj.AddComponent<DemoWeapon>());

		weaponPositions.Add(new Vector2(-1, 1.25f));
		weaponPositions.Add(new Vector2(-1, -1.25f));
		weaponPositions.Add(new Vector2(3.5f, 2.5f));
		weaponPositions.Add(new Vector2(3.5f, -3));
	}

	void Update() {
		if (!ShipFightManager.paused) {
			ShipUpdate();
		}
	}
}
