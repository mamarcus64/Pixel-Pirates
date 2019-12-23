using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShip : Ship {

    public BasicShip Init(Vector2 location, Ship enemy, Player player)
    {
        base.Init(SpritePath.demoShip, new Vector2(12.8f, 4.32f), location, 5, enemy, player);
        roomManager.SetOffset(new Vector2(0, -0.5f));
        List<Vector2> weaponPos = WeaponLayout();
        weaponManager.Add(obj.AddComponent<DemoWeapon>().Init(weaponPos[0], this));
        weaponManager.Add(obj.AddComponent<DemoWeapon>().Init(weaponPos[1], this));
        return this;
    }

    void Update()
    {
        if (!ShipFightManager.paused)
        {
            ShipUpdate();
        }
    }


    override public List<Vector2> WeaponLayout()
    {
        List<Vector2> weaponPositions = new List<Vector2>();
        weaponPositions.Add(new Vector2(-1, 1.25f));
        weaponPositions.Add(new Vector2(-1, -1.25f));
        weaponPositions.Add(new Vector2(3.5f, 2.5f));
        weaponPositions.Add(new Vector2(3.5f, -3));
        return weaponPositions;
    }
    override public List<Vector3> RoomLayout()
    {
        List<Vector3> roomPositions = new List<Vector3>();
        roomPositions.Add(new Vector3(-1, -1, 1));
        roomPositions.Add(new Vector3(-1, 0, 1));
        roomPositions.Add(new Vector3(-1, 2, 2));
        roomPositions.Add(new Vector3(0, 2, 2));
        return roomPositions;
    }
}
