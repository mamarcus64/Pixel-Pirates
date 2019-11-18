using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : EntityManager<Room>
{
    public static int gridWidth = 40;
    public static int gridHeight = 20;
    public Vector2 offset = new Vector2(0, 0);
    bool offsetSet = false;
    public RoomManager(Entity owner) : base(owner)
    {

    }

    public RoomManager(Entity owner, Vector2 offset) : base(owner)
    {
        this.offset = offset;
    }

    public bool[][] GetRoomLayout()
    {
        bool[][] result = new bool[gridWidth][];
        for(int i = 0; i < result.Length; i++)
            result[i] = new bool[gridHeight];

        foreach (Vector2 cell in GetAllCells()) {
            Vector2 grid = WorldToGrid(cell.x, cell.y);
            result[(int)(grid.x)][(int)(grid.y)] = true;
        }
        return result;
    }

    public List<Vector2> GetAllCells()
    {
        List<Vector2> result = new List<Vector2>();
        foreach (Entity entity in entities)
        {
            Room room = entity as Room;
            if (room.GetCells().Count == 0)
                room.SetSpots();
            foreach (Vector2 cell in room.GetCells())
            {
                result.Add(cell);
            }
        }
        return result;
    }

    public void SetOffset(Vector2 offset)
    {
        this.offset = offset;
        offsetSet = true;
    }

    public void SetOffset(float xOffset, float yOffset)
    {
        SetOffset(new Vector2(xOffset, yOffset));
    }

    public Vector2 WorldToGrid(Vector2 world)
    {
        return WorldToGrid(world.x, world.y);
    }

    public Vector2 WorldToGrid(float xWorld, float yWorld)
    {
        if (!offsetSet)
        {
            //I'm too lazy to find the absolute value function and/or if modulus works with floats so I'm doing this instead
            while ((xWorld > 0 ? xWorld : xWorld * -1) >= Room.cellWidth)
                xWorld -= (xWorld > 0 ? Room.cellWidth : Room.cellWidth * -1);
            while ((yWorld > 0 ? yWorld : yWorld * -1) >= Room.cellHeight)
                yWorld -= (yWorld > 0 ? Room.cellHeight : Room.cellHeight * -1);
            offset = new Vector2(-xWorld, -yWorld);
            offsetSet = true;
        }
        xWorld -= offset.x;
        yWorld -= offset.y;
        if (xWorld < 0)
            xWorld -= (Room.cellWidth - 0.001f);
        else
            xWorld += 0.001f;
        if (yWorld < 0)
            yWorld -= (Room.cellHeight - 0.001f);
        else
            yWorld += 0.001f;
        //because integer division truncates, need to subtract almost one to have opposite effect for negative numbers
        float xGrid = gridWidth / 2 + (int)(xWorld / Room.cellWidth);
        float yGrid = gridHeight / 2 + (int)(yWorld / Room.cellHeight);
        return new Vector2(xGrid, yGrid);
    }

    public Vector2 GridToWorld(Vector2 grid)
    {
        return GridToWorld(grid.x, grid.y);
    }

    public Vector2 GridToWorld(float xGrid, float yGrid)
    {
        float xWorld = (xGrid - gridWidth / 2) * Room.cellWidth + offset.x;
        float yWorld = (yGrid - gridHeight / 2) * Room.cellHeight + offset.y;
        return new Vector2(xWorld, yWorld);
    }
}
