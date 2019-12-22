using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Room : Entity
{
    Icon roomIcon;
    Entity system;
    List<Vector2> cells;
    CrewMember[] crew = new CrewMember[4];
    public static float cellWidth = 0.5f;
    public static float cellHeight = 0.5f;
    public Room Init(string spritePath, Vector2 location, Entity parent)
    {
        base.Init(spritePath, new Vector2(cellWidth * (1 + Convert.ToInt32(this.GetType().Name == "LongRoom" || this.GetType().Name == "BigRoom")),
            cellHeight * (1 + Convert.ToInt32(this.GetType().Name == "TallRoom" || this.GetType().Name == "BigRoom"))), location, "Rooms", 2, parent);
        cells = new List<Vector2>();
        wantsFocus = true;
        roomIcon = obj.AddComponent<Icon>().Init("", new Vector2(cellWidth, cellHeight), new Vector2(0, 0), "Rooms.1", this, false);
        return this;
    }

    public void SetSpots()
    {
        switch(this.GetType().Name)
        {
            case "SingleRoom":
                cells.Add(new Vector2(relativePosition.x, relativePosition.y));
                break;
            case "LongRoom":
                cells.Add(new Vector2(relativePosition.x - cellWidth / 2, relativePosition.y));
                cells.Add(new Vector2(relativePosition.x + cellWidth / 2, relativePosition.y));
                break;
            case "TallRoom":
                cells.Add(new Vector2(relativePosition.x, relativePosition.y + cellHeight / 2));
                cells.Add(new Vector2(relativePosition.x, relativePosition.y - cellHeight / 2));
                break;
            case "BigRoom":
                cells.Add(new Vector2(relativePosition.x + cellWidth / 2, relativePosition.y + cellHeight / 2));
                cells.Add(new Vector2(relativePosition.x + cellWidth / 2, relativePosition.y - cellHeight / 2));
                cells.Add(new Vector2(relativePosition.x - cellWidth / 2, relativePosition.y + cellHeight / 2));
                cells.Add(new Vector2(relativePosition.x - cellWidth / 2, relativePosition.y - cellHeight / 2));
                break;
        }
        crew = new CrewMember[cells.Count];
        /* The array crew contains an index for each cell in a given room; unless there is a crew that is either in the room or 
         * going to the room, that index will be null; as soon as a crew is given the move command to a certain room, the first available
         * cell in the room will be assigned to that crew; when the crew leaves, that spot will be null again. To test if the crew is actually
         * in the room, a Vector2 Distance will be calculated based on the cell spot and the crew spot
         */
    }

    public bool HasSpot()
    {
        if (cells.Count == 0)
            SetSpots();
        for (int i = 0; i < cells.Count; i++)
            if (crew[i] == null)
                return true;
        return false;
    }
    public Vector2 GetSpot()
    {
        if (cells.Count == 0)
            SetSpots();
        for (int i = 0; i < cells.Count; i++)
            if (crew[i] == null)
                return cells[i];
        return new Vector2(100, 100);
        //first available empty cell spot
    }

    public List<Vector2> GetCells()
    {
        return cells;
    }

    /// <summary>
    /// adds given crew to the first available spot in the cells list
    /// </summary>
    /// <param name="member"></param>
    /// <returns></returns>
    public Vector2 AddCrew(CrewMember member)
    {
        if (cells.Count == 0)
            SetSpots();
        for (int i = 0; i < cells.Count; i++)
            if (crew[i] == null) {
                crew[i] = member;
                return cells[i];
            }
        return new Vector2(100, 100);
    }

    public Vector2 RemoveCrew(CrewMember member)
    {
        if (cells.Count == 0)
            SetSpots();
        for (int i = 0; i < cells.Count; i++)
            if (crew[i] == member)
            {
                crew[i] = null;
                return cells[i];
            }
        return new Vector2(100, 100);
    }

    public void AttachWeapon(Weapon weapon)
    {
        system = weapon;
        roomIcon.ChangeSprite(SpritePath.cannonIcon);
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
