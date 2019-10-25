using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : EntityManager
{
    public static int gridWidth = 80;
    public static int gridHeight = 40;
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
        foreach (Entity entity in entities)
        {
            Room room = entity as Room;
            foreach (Vector2 cell in room.GetCells()) {
                Vector2 grid = WorldToGrid(cell.x, cell.y);
                Vector2 world = GridToWorld(grid);
                result[(int)(grid.x)][(int)(grid.y)] = true;
            }
        }
        return result;
    }

    public Vector2 WorldToGrid(Vector2 world)
    {
        return WorldToGrid(world.x, world.y);
    }

    public Vector2 WorldToGrid(float xWorld, float yWorld)
    {
        if (xWorld < 0)
            xWorld -= (Room.cellWidth - 0.001f);
        if (yWorld < 0)
            yWorld -= (Room.cellHeight - 0.001f);
        //because integer division truncates, need to subtract almost one to have opposite effect for negative numbers
        float xGrid = gridWidth / 2 + (int)(xWorld / Room.cellWidth);
        float yGrid = gridHeight / 2 + (int)(yWorld / Room.cellHeight);
        if(!offsetSet)
        {
            //I'm too lazy to find the absolute value function and/or if modulus works with floats so I'm doing this instead
            while ((xWorld > 0 ? xWorld : xWorld * -1) > Room.cellWidth)
                xWorld -= (xWorld > 0 ? Room.cellWidth : Room.cellWidth * -1);
            while ((yWorld > 0 ? yWorld : yWorld * -1) > Room.cellHeight)
                yWorld -= (yWorld > 0 ? Room.cellHeight : Room.cellHeight * -1);
            offset = new Vector2(-xWorld, -yWorld);
            offsetSet = true;
        }
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
