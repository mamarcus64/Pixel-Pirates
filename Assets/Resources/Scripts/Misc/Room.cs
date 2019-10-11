using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Room : Entity
{
    GameObject roomIcon;
    Entity system;
    public void RoomStart()
    {
        width = 0.5f * (1 + Convert.ToInt32(this.GetType().Name == "LongRoom" || this.GetType().Name == "BigRoom"));
        height = 0.5f * (1 + Convert.ToInt32(this.GetType().Name == "TallRoom" || this.GetType().Name == "BigRoom"));
        layer = "Rooms";
        wantsFocus = true;
        roomIcon = new GameObject();
        roomIcon.AddComponent<SpriteRenderer>();
        EntityStart();
    }
    public void AttachWeapon(Weapon weapon)
    {
        system = weapon;
        roomIcon.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Room Icons/cannon icon");
        roomIcon.transform.parent = obj.transform;
        SetLocation(0, 0, -0.01f, roomIcon);
        Resize(0.5f, 0.5f, roomIcon);
    }
    public void RoomUpdate()
    {
        EntityUpdate();
    }

    public override void TakeDamage(int damage)
    {
        (obj.transform.parent.GetComponent<EntityProxy>().GetEntity() as Ship).TakeDamage(damage);
        if (system != null)
        {
            system.TakeDamage(1);
        }
    }
}
