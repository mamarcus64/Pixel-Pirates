using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path
{
    List<Vector2> points = new List<Vector2>();
    int currentPoint = 0;

    public Path()
    {
       
    }

    public Path(Vector2 point)
    {
        points.Add(point);
    }

    public Path(List<Vector2> points)
    {
        this.points = points;
    }
    public Vector2 CurrentPoint()
    {
        return points[currentPoint];
    }

    public bool hasNext()
    {
        return currentPoint < points.Count - 1;
    }

    public bool hasCurrent()
    {
        return currentPoint < points.Count;
    }

    public Vector2 NextPoint()
    {
        currentPoint++;
        return points[currentPoint];
    }

    public static Path GetPath(RoomManager roomManager, Vector2 vectorStart, Vector2 vectorEnd)
    {
        Node[][] grid = ToNodeGrid(roomManager.GetRoomLayout(), roomManager);
        Node start = new Node(vectorStart);
        Debug.Log("start pos:" + new Vector2(start.x, start.y));
        Node end = new Node(vectorEnd);
        Debug.Log("end pos:" + new Vector2(end.x, end.y));
        List<Node> path = new List<Node>();
        List<Node> openSet = new List<Node>();
        List<Node> closedSet = new List<Node>();
        openSet.Add(start);
        while (openSet.Count > 0)
        {
            Node current = openSet[0];
            foreach (Node node in openSet)
                if (node.GetF() < current.GetF())
                    current = node;
            current.AddNeighbors(grid, roomManager.WorldToGrid(current.x, current.y));
            openSet.Remove(current);
            closedSet.Add(current);
            path = new List<Node>(); //re-initialize every time as it finds a better path
            path.Add(current);
            Node parent = current.previous;
            while (parent != null)
            {
                path.Add(parent);
                parent = parent.previous;
            }
            if (current == end)
            {
                break;
            }
            foreach (Node neighbor in current.neighbors)
            {
                if (closedSet.Contains(neighbor) || neighbor.isWall)
                    continue;
                if (Distance(neighbor, current) > 1) //diagonal
                {
                    Vector2 currentPoint = roomManager.WorldToGrid(current.x, current.y);
                    Vector2 neighborPoint = roomManager.WorldToGrid(neighbor.x, neighbor.y);
                    if (grid[(int)(currentPoint.x)][(int)(neighborPoint.y)].isWall ||
                        grid[(int)(neighborPoint.x)][(int)(currentPoint.y)].isWall)
                        continue;
                }
                float tempG = current.g + Distance(neighbor, current);
                if (openSet.Contains(neighbor))
                {
                    if (neighbor.g > tempG)
                    {
                        neighbor.g = tempG;
                        neighbor.previous = current;
                    }
                    else
                        continue;
                }
                else
                {
                    neighbor.g = tempG;
                    openSet.Add(neighbor);
                    neighbor.h = Distance(neighbor, end);
                    neighbor.previous = current;
                }
            }
        }
        path.Reverse();
        List<Vector2> vectorPath = new List<Vector2>();
        foreach (Node node in path)
        {
            vectorPath.Add(node.ToVector());
            Debug.Log("Node: " + node.ToVector() + ", grid: " + roomManager.WorldToGrid(node.ToVector()));
        }
        Path result = new Path(vectorPath);
        return result;
    }

    static float Distance(Node a, Node b)
    {
        return (Mathf.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y)));
    }

    static Node[][] ToNodeGrid(bool[][] grid, RoomManager roomManager)
    {
        Node[][] result = new Node[grid.Length][];
        for (int i = 0; i < result.Length; i++)
            result[i] = new Node[grid[0].Length];
        for (int i = 0; i < result.Length; i++)
            for (int j = 0; j < result[0].Length; j++)
            {
                result[i][j] = new Node(roomManager.GridToWorld(i, j));
                result[i][j].isWall = false;// !grid[i][j];
            }
        return result;
    }
}

class Node
{
    public float x;
    public float y; //actual coordinates in space
    public float g = 100000;
    public float h = 100000;
    public bool isWall = false;
    public List<Node> neighbors = new List<Node>();
    public Node previous = null;

    public Node (Vector2 position)
    {
        x = position.x;
        y = position.y;
    }

    public Node(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public Vector2 ToVector()
    {
        return new Vector2(x, y);
    }
    public void AddNeighbors(Node[][] grid, Vector2 gridPoint)
    {
        if (gridPoint.x > 0)
            neighbors.Add(grid[(int)(gridPoint.x - 1)][(int)(gridPoint.y)]);
        if (gridPoint.x < grid.Length - 1)
            neighbors.Add(grid[(int)(gridPoint.x + 1)][(int)(gridPoint.y)]);
        if (gridPoint.y > 0)
            neighbors.Add(grid[(int)(gridPoint.x)][(int)(gridPoint.y - 1)]);
        if (gridPoint.y < grid[0].Length - 1)
            neighbors.Add(grid[(int)(gridPoint.x)][(int)(gridPoint.y + 1)]);
        if (gridPoint.x > 0 && gridPoint.y > 0)
            neighbors.Add(grid[(int)(gridPoint.x - 1)][(int)(gridPoint.y - 1)]);
        if (gridPoint.x > 0 && gridPoint.y < grid[0].Length - 1)
            neighbors.Add(grid[(int)(gridPoint.x - 1)][(int)(gridPoint.y + 1)]);
        if (gridPoint.x < grid.Length - 1 && gridPoint.y > 0)
            neighbors.Add(grid[(int)(gridPoint.x + 1)][(int)(gridPoint.y - 1)]);
        if (gridPoint.x < grid.Length - 1 && gridPoint.y < grid[0].Length - 1)
            neighbors.Add(grid[(int)(gridPoint.x + 1)][(int)(gridPoint.y + 1)]);
    }

    public float GetF() { return g + h; }
}
