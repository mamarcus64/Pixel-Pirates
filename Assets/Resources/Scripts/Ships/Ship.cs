using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : Entity {
	protected WeaponManager weaponManager;
	protected RoomManager roomManager;
	protected CrewManager crewManager;
	protected List<Icon> healthBar = new List<Icon>();
	protected Shield shield;
	protected Player player;

	public Ship Init(string spritepath, Vector2 size, Vector2 location, int health, Player player) {
		base.Init(spritepath, size, location, "Ships", health);
		this.player = player;
		List<Vector3> roomPositions = RoomLayout();
		weaponManager = new WeaponManager(this);
		roomManager = new RoomManager(this);
		crewManager = new CrewManager(this);
		wantsFocus = false;
		foreach (Vector3 room in roomPositions) {
			switch (room.z) {
				case 0:
					roomManager.Add(obj.AddComponent<SingleRoom>().Init(RoomGridToWorld(room), this));
					break;
				case 1:
					roomManager.Add(obj.AddComponent<LongRoom>().Init(RoomGridToWorld(room), this));
					break;
				case 2:
					roomManager.Add(obj.AddComponent<TallRoom>().Init(RoomGridToWorld(room), this));
					break;
				case 3:
					roomManager.Add(obj.AddComponent<BigRoom>().Init(RoomGridToWorld(room), this));
					break;
			}
		}

		for (int i = 0; i < health; i++) {
			healthBar.Add(obj.AddComponent<Icon>().Init(SpritePath.greenBar, new Vector2(0.25f, 0.25f), new Vector2(-width / 3 + 0.3f * i, height / 3), "Green Bar", this));
		}

		crewManager.Add(obj.AddComponent<BasicCrew>().Init(this, roomManager.Get(0)));
		crewManager.Add(obj.AddComponent<BasicCrew>().Init(this, roomManager.Get(1)));
		shield = obj.AddComponent<Shield>().Init(this);
		StartCoroutine(Load());
		return this;
	}

	IEnumerator Load() {
		//fix later - should not be load function anymore
		yield return new WaitForSeconds(Entity.bufferTime);
		weaponManager.Get(0).SetParent(this);
		weaponManager.Get(1).SetParent(this);
		weaponManager.Place(WeaponLayout()[0], 0);
		weaponManager.Place(WeaponLayout()[1], 1);
		(roomManager.Get(0) as Room).AttachWeapon(weaponManager.Get(0) as Weapon);
		(roomManager.Get(1) as Room).AttachWeapon(weaponManager.Get(1) as Weapon);
	}

	public abstract List<Vector3> RoomLayout();
	public abstract List<Vector2> WeaponLayout();

	public RoomManager GetRoomManager() {
		return roomManager;
	}

	public List<Room> GetRooms() {
		return roomManager.GetAll();
	}

	public List<Weapon> GetWeapons() {
		return weaponManager.GetAll();
	}

	public override void TakeDamage(int damage) {
		health -= damage;
		for (int i = 0; i < damage; i++) {
			if (healthBar.Count == 0)
				break;
			healthBar[healthBar.Count - 1].Die();
			healthBar.RemoveAt(healthBar.Count - 1);
		}
		if (health == 0)
			Die();
	}

	public void ShipUpdate() {
		EntityUpdate();
		player.Play(ShipFightManager.GetPlayerShip(), ShipFightManager.GetEnemyShip());
	}

	public Vector2 RoomGridToWorld(Vector3 roomPosition) {
		switch (roomPosition.z) {
			case 0:
				return new Vector2(roomPosition.x * Room.cellWidth + roomManager.offset.x,
					roomPosition.y * Room.cellWidth + roomManager.offset.y);
			case 1:
				return new Vector2((roomPosition.x + 0.5f) * Room.cellWidth + roomManager.offset.x,
					roomPosition.y * Room.cellWidth + roomManager.offset.y);
			case 2:
				return new Vector2(roomPosition.x * Room.cellWidth + roomManager.offset.x,
					(roomPosition.y - 0.5f) * Room.cellWidth + roomManager.offset.y);
			case 3:
				return new Vector2((roomPosition.x + 0.5f) * Room.cellWidth + roomManager.offset.x,
					(roomPosition.y - 0.5f) * Room.cellWidth + roomManager.offset.y);
			default:
				return new Vector2(0, 0);
		}
	}

}
