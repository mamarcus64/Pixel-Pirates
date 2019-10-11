using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : Entity
{
    protected WeaponManager weaponManager;
    protected List<Vector2> weaponPositions = new List<Vector2>();
    protected RoomManager roomManager;
    protected List<Vector3> roomPositions = new List<Vector3>();
    protected List<GameObject> healthBar = new List<GameObject>();
    protected Shield shield;
    CrewMember crew;
    public void ShipStart()
    {
        weaponManager = new WeaponManager(this);
        roomManager = new RoomManager(this);
        layer = "Ships";
        EntityStart();
        wantsFocus = false;
        foreach (Vector3 room in roomPositions)
        {
            if(room.z == 0)
                roomManager.Add(obj.AddComponent<SingleRoom>());
            else if (room.z == 1)
                roomManager.Add(obj.AddComponent<LongRoom>());
            else if (room.z == 2)
                roomManager.Add(obj.AddComponent<TallRoom>());
            else if (room.z == 3)
                roomManager.Add(obj.AddComponent<BigRoom>());

        }
        for (int i = 0; i < health; i++) {
            healthBar.Add(new GameObject("Health"));
            healthBar[i].AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Misc/green bar");
        }
        crew = obj.AddComponent<CrewMember>();
        shield = obj.AddComponent<Shield>();
        shield.SetShip(this);
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        yield return new WaitForSeconds(Entity.bufferTime);
        weaponManager.Get(0).SetParent(this);
        weaponManager.Get(1).SetParent(this);
        weaponManager.Place(weaponPositions[0], 0);
        weaponManager.Place(weaponPositions[1], 1);
       
        for (int i = 0; i < roomManager.Size(); i++)
        {
            roomManager.Get(i).SetParent(this);
            roomManager.Get(i).SetLocation(roomPositions[i].x * roomManager.Get(i).getWidth(), 
                roomPositions[i].y * roomManager.Get(i).GetHeight());
        }
        for (int i = 0; i < healthBar.Count; i++)
        {
            healthBar[i].transform.parent = obj.transform;
            Resize(0.25f, 0.25f, healthBar[i]);
            SetLocation(-width / 3 + 0.3f * i, height / 3, GetZPosition("Green Bar"), healthBar[i]);
        }
        (roomManager.Get(0) as Room).AttachWeapon(weaponManager.Get(0) as Weapon);
        (roomManager.Get(1) as Room).AttachWeapon(weaponManager.Get(1) as Weapon);

        crew.SetParent(this);
        crew.SetPlayerOwned(true);
        crew.GoToRoom(roomManager.Get(2) as Room);
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
    
    public override void OnFocusClick(Entity entity)
    {
        
    }
}
