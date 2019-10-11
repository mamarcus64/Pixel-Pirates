using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewMember : Entity
{
    void Start()
    {
        health = 10;
        layer = "Crew";
        spritePath = "Sprites/Misc/green bar";
        width = 0.5f;
        height = 0.5f;
        wantsFocus = true;
        EntityStart();
    }
    void Update()
    {
        if (!ShipFightManager.paused)
        {
            EntityUpdate();
        }
    }

    public override void OnFocusClick(Entity entity)
    {
        Debug.Log("crew clicked");
        if (entity is Room room)
        {
            GoToRoom(room);
       }
    }

    public void GoToRoom(Room room)
    {
        this.SetLocation(room.GetLocalPosition());   
    }
}
