using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewMember : Entity
{
    protected Path path;
    protected Ship ship;
    protected Room currentRoom;
    protected float speed = 2.5f;
    void Start()
    {
        health = 10;
        layer = "Crew";
        spritePath = "Sprites/Crew/demo crew";
        width = 0.5f;
        height = 0.5f;
        wantsFocus = true;
        path = null;
        EntityStart();
    }
    int x = 0;
    void Update()
    {

        if (!ShipFightManager.paused)
        {
            EntityUpdate();
            if (path != null && path.HasCurrent())
            {
                Move();
            }
        }
    }

    public override void OnFocusClick(Entity entity)
    {
        if (entity is Room room)
        {
            if(room != currentRoom)
                GoToRoom(room);
       }
    }

    public void Move()
    {
            Vector2 direction = new Vector2(-this.localPosition.x + path.CurrentPoint().x, -this.localPosition.y + path.CurrentPoint().y).normalized;
            Vector2 magnitude = new Vector2(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime);
        if (Vector2.Distance(path.CurrentPoint(), localPosition) <= magnitude.magnitude)
            {
                
                SetLocation(path.CurrentPoint().x, path.CurrentPoint().y);
            if (path.HasNext())
            {
                path.NextPoint();// Debug.Log(path.NextPoint());
            }
            else
            {
                //Debug.Log(path);
                path = null;
                //Debug.Log("path finished");
            }

            }
            SetLocation(localPosition.x + magnitude.x, localPosition.y + magnitude.y);
    }

    public void SetShip(Ship ship)
    {
        this.ship = ship;
    }

    public void SetRoom(Room room)
    {
        currentRoom = room;
        currentRoom.AddCrew(this);
    }

    public void GoToRoom(Room room)
    {
        if (currentRoom == null)
            path = Path.GetPath(ship.GetRoomManager(), new Vector2(0, 0), room.AddCrew(this));
        else
            path = Path.GetPath(ship.GetRoomManager(), currentRoom.RemoveCrew(this), room.AddCrew(this));
        currentRoom = room;
    }
}
