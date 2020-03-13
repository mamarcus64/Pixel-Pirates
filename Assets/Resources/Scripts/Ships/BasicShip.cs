using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShip : Ship {

    public BasicShip Init(Vector2 location, List<Weapon> weapons, List<CrewMember> crew, Player player) {
		base.Init(SpritePath.andyShip, new Vector2(12.8f, 4.32f), location, weapons, crew, 5, player);
		roomManager.SetOffset(new Vector2(0, -0.5f));
		return this;
	}

	void Update() {
		if (!GameManager.paused) {
			ShipUpdate();
		}
	}

	override public List<Vector2> WeaponLayout() {
		List<Vector2> weaponPositions = new List<Vector2>();
		weaponPositions.Add(new Vector2(-1, 1.25f));
		weaponPositions.Add(new Vector2(-1, -1.25f));
		weaponPositions.Add(new Vector2(3.5f, 2.5f));
		weaponPositions.Add(new Vector2(3.5f, -3));
		return weaponPositions;
	}

    override public List<Room> WeaponRoomLayout() {
        List<Room> rooms = new List<Room>();
        rooms.Add(roomManager.Get(3));
        rooms.Add(roomManager.Get(2));
        rooms.Add(roomManager.Get(0));
        rooms.Add(roomManager.Get(1));
        return rooms;
    }
	override public List<Vector3> RoomLayout() {
		List<Vector3> roomPositions = new List<Vector3>();
		roomPositions.Add(new Vector3(-1, -1, 1));
		roomPositions.Add(new Vector3(-1, 0, 1));
		roomPositions.Add(new Vector3(-1, 2, 2));
		roomPositions.Add(new Vector3(0, 2, 2));
		return roomPositions;
	}

    override public List<int> CrewSpawnLayout() {
        List<int> spawnPositions = new List<int>();
        spawnPositions.Add(0);
        spawnPositions.Add(1);
        spawnPositions.Add(0);
        spawnPositions.Add(1);
        return spawnPositions;
    }
    public override string GetDisplayName() {
        return "Basic Ship";
    }
}
