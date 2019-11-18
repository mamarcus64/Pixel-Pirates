using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : Entity
{
    protected WeaponManager weaponManager;
    protected RoomManager roomManager;
    protected List<GameObject> healthBar = new List<GameObject>();
    protected Shield shield;
    BasicCrew crew;
    BasicCrew crew2ElectricBugaloo;

    public Ship Init(string spritepath, Vector2 size, Vector2 location, int health)
    {  
        base.Init(spritepath, size, location, "Ships", health);
        List<Vector3> roomPositions = RoomLayout();
        weaponManager = new WeaponManager(this);
        roomManager = new RoomManager(this);
        wantsFocus = false;
        foreach (Vector3 room in roomPositions)
        {
            if (room.z == 0)
                roomManager.Add(obj.AddComponent<SingleRoom>().Init(RoomGridToWorld(room), this));
            else if (room.z == 1)
                roomManager.Add(obj.AddComponent<LongRoom>().Init(RoomGridToWorld(room), this));
            else if (room.z == 2)
                roomManager.Add(obj.AddComponent<TallRoom>().Init(RoomGridToWorld(room), this));
            else if (room.z == 3)
                roomManager.Add(obj.AddComponent<BigRoom>().Init(RoomGridToWorld(room), this));

        }
        for (int i = 0; i < health; i++)
        {
            healthBar.Add(new GameObject("Health"));
            healthBar[i].AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Misc/green bar");
        }
        crew = obj.AddComponent<BasicCrew>().Init(this, roomManager.Get(0));
        crew2ElectricBugaloo = obj.AddComponent<BasicCrew>().Init(this, roomManager.Get(1));
        shield = obj.AddComponent<Shield>().Init(this);
        StartCoroutine(Load());
        return this;
    }

    IEnumerator Load()
    {
        yield return new WaitForSeconds(Entity.bufferTime);
        weaponManager.Get(0).SetParent(this);
        weaponManager.Get(1).SetParent(this);
        weaponManager.Place(WeaponLayout()[0], 0);
        weaponManager.Place(WeaponLayout()[1], 1);
       
       
        for (int i = 0; i < healthBar.Count; i++)
        {
            healthBar[i].transform.parent = obj.transform;
            Resize(0.25f, 0.25f, healthBar[i]);
            SetLocation(-width / 3 + 0.3f * i, height / 3, GetZPosition("Green Bar"), healthBar[i]);
        }
        (roomManager.Get(0) as Room).AttachWeapon(weaponManager.Get(0) as Weapon);
        (roomManager.Get(1) as Room).AttachWeapon(weaponManager.Get(1) as Weapon);
    }

    public abstract List<Vector3> RoomLayout();
    public abstract List<Vector2> WeaponLayout();

    public RoomManager GetRoomManager()
    {
        return roomManager;
    }

    public override void TakeDamage(int damage)
    {
        health -= damage;
        for (int i = 0; i < damage; i++) {
            if (healthBar.Count == 0)
                break;
            Destroy(healthBar[healthBar.Count - 1]);
            healthBar.RemoveAt(healthBar.Count - 1);
                }
        for (int i = 0; i < healthBar.Count; i++)
        {
            healthBar[i].transform.parent = obj.transform;
            Resize(0.25f, 0.25f, healthBar[i]);
            SetLocation(-width / 3 + 0.3f * i, height / 3, GetZPosition("Green Bar"), healthBar[i]);
        }
        if (health == 0)
            Destroy(obj);
    }

    public void ShipUpdate()
    {
        EntityUpdate();
    }

    public List<Room> getRooms()
    {
        List<Room> rooms = new List<Room>();
        foreach (Entity e in roomManager.GetAll())
            rooms.Add(e as Room);
        return rooms;
    }

    public Vector2 RoomGridToWorld(Vector3 roomPosition)
    {
        switch(roomPosition.z)
        {
            case (0):
                return new Vector2(roomPosition.x * Room.cellWidth + roomManager.offset.x, 
                    roomPosition.y * Room.cellWidth + roomManager.offset.y);
            case (1):
                return new Vector2((roomPosition.x + 0.5f) * Room.cellWidth + roomManager.offset.x,
                    roomPosition.y * Room.cellWidth + roomManager.offset.y);
            case (2):
                return new Vector2(roomPosition.x * Room.cellWidth + roomManager.offset.x,
                    (roomPosition.y - 0.5f) * Room.cellWidth + roomManager.offset.y);
            case (3):
                return new Vector2((roomPosition.x + 0.5f) * Room.cellWidth + roomManager.offset.x,
                    (roomPosition.y - 0.5f) * Room.cellWidth + roomManager.offset.y);
            default:
                return new Vector2(0, 0);
        }
    }
    
}
