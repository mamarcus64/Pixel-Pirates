using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CrewMember : Entity
{
    protected Path path;
    protected Ship ship;
    protected Room currentRoom;
    protected float speed = 0.5f;
    public CrewMember Init(string spritePath, Ship ship, Room room)
    {
        base.Init(spritePath, new Vector2(0.5f, 0.5f), room.AddCrew(this), "Crew", 10, ship);
        currentRoom = room;
        wantsFocus = true;
        path = null;
        SetShip(ship);
        return this;
    }

    public void CrewUpdate()
    {
            EntityUpdate();
            if (path != null && path.HasCurrent())
            {
                Move();
            }      
    }

    public override void OnFocusLost(Entity entity)
    {
        if (entity is Room room)
        {
            if(room != currentRoom)
                GoToRoom(room);
       }
    }

    public void Move()
    {
        Vector2 direction = new Vector2(-this.relativePosition.x + path.CurrentPoint().x, -this.relativePosition.y + path.CurrentPoint().y).normalized;
        Vector2 directionWithMagnitude = new Vector2(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime);
        if (Vector2.Distance(path.CurrentPoint(), relativePosition) <= directionWithMagnitude.magnitude ||
            Vector2.Distance(path.CurrentPoint(), relativePosition) <= 0.01f) //sometimes directionWithMagnitude is 0 and distance is like 1E-9
        {
            SetLocation(path.CurrentPoint().x, path.CurrentPoint().y);
            if (path.HasNext())
                path.NextPoint();
            else
                path = null;
        }
        SetLocation(relativePosition.x + directionWithMagnitude.x, relativePosition.y + directionWithMagnitude.y);
    }

    public void SetShip(Ship ship)
    {
        this.ship = ship;
        SetParent(ship);
    }

    public void SetRoom(Room room)
    {
        currentRoom = room;
        currentRoom.AddCrew(this);
    }

    public void GoToRoom(Room room)
    {
        Vector2 removedLocation = currentRoom.RemoveCrew(this);
        if (currentRoom == null) //just in case something goes terribly wrong
            path = Path.GetPath(ship.GetRoomManager(), new Vector2(0, 0), room.AddCrew(this));
        else if (Vector2.Distance(removedLocation, relativePosition) < Room.cellWidth / 2) //if crew has fully completed its previous path
            path = Path.GetPath(ship.GetRoomManager(), removedLocation, room.AddCrew(this));
        else //if the crew is redirected while still traveling on its previous path
        {
            Vector2 closestCell = new Vector2(0, 0);
            foreach (Vector2 cell in ship.GetRoomManager().GetAllCells())
                if (Vector2.Distance(cell, relativePosition) < Vector2.Distance(closestCell, relativePosition))
                    closestCell = cell;
            path = Path.GetPath(ship.GetRoomManager(), closestCell, room.AddCrew(this));
        }
        currentRoom = room;
    }

    public void TeleportToRoom(Room room)
    {
        if(currentRoom != null)
            currentRoom.RemoveCrew(this);
        SetLocation(room.AddCrew(this));
        currentRoom = room;
    }
}
