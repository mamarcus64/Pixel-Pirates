using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Path is the class that represents crew movement; very similar to a single linked list
/// </summary>
public class Path
{
    List<Vector2> points = new List<Vector2>();
    int currentPoint = 0;

    override
    public string ToString()
    {
        string result = "";
        foreach (Vector2 point in points)
            result += point + "  ";
        return result;
    }

    public Path()
    {
       
    }

    public Path(List<Vector2> points)
    {
        this.points = points;
    }
    public Vector2 CurrentPoint()
    {
        return points[currentPoint];
    }

    public bool HasNext()
    {
        return currentPoint < points.Count - 1;
    }

    public bool HasCurrent()
    {
        return currentPoint < points.Count;
    }

    public Vector2 NextPoint()
    {
        currentPoint++;
        return points[currentPoint];
    }
    /// <summary>
    /// uses A-star path-finding
    /// NOTE: look at the conversion between "world" and "grid" coordinate systems - very important to understand
    /// "world" coordinate is the local position of each room cell relative to the parent ship
    /// "grid" coordinate is changing that to a positive integer to be used in a 2D array
    /// </summary>
    /// <param name="roomManager"> pass in a room manager so that the room offset for the particular ship is kept</param>
    /// <param name="vectorStart"> start of path, in WORLD coordinates </param>
    /// <param name="vectorEnd"> end of path, in WORLD coordinates </param>
    /// <returns></returns>
    public static Path GetPath(RoomManager roomManager, Vector2 vectorStart, Vector2 vectorEnd)
    {
        Node[][] grid = ToNodeGrid(roomManager.GetRoomLayout(), roomManager);
        Node start = new Node(vectorStart);
        Node end = new Node(vectorEnd);
        start.g = 0;
        start.h = Vector2.Distance(start.ToVector(), end.ToVector());
        if(DebugToggler.pathCreated)
            Debug.Log("start pos:" + new Vector2(start.x, start.y) + ", end pos:" + new Vector2(end.x, end.y));
        //pretty standard A-star code, look it up online for more detail as to what is going on
        List<Node> path = new List<Node>();
        List<Node> openSet = new List<Node>();
        List<Node> closedSet = new List<Node>();
        openSet.Add(start);
        while (openSet.Count > 0)
        {
            Node current = openSet[0];
            foreach (Node node in openSet) {
                if (node.GetF() < current.GetF())
                    current = node;
                //^^same as just selecting node with lowest F-score in openSet
            }
            if (Vector2.Distance(current.ToVector(), end.ToVector()) < 0.01f) //I can't use .equals() or == b/c float math has rounding errors
            {
                
                path.Add(current);
                Node parent = current.previous;
                while (parent != null)
                {
                    path.Add(parent);
                    parent = parent.previous;
                }
                break;
            }
            current.AddNeighbors(grid, roomManager.WorldToGrid(current.x, current.y));
            closedSet.Add(current);
            openSet.Remove(current);
            foreach (Node neighbor in current.neighbors)
            {
                if (neighbor.isWall)
                    closedSet.Add(neighbor);
                if (closedSet.Contains(neighbor))
                    continue;
                if (Distance(roomManager.WorldToGrid(neighbor.ToVector()), roomManager.WorldToGrid(current.ToVector())) > 1) //diagonal-neighbor
                {
                    Vector2 currentPoint = roomManager.WorldToGrid(current.x, current.y);
                    Vector2 neighborPoint = roomManager.WorldToGrid(neighbor.x, neighbor.y);
                    if (grid[(int)(currentPoint.x)][(int)(neighborPoint.y)].isWall ||
                        grid[(int)(neighborPoint.x)][(int)(currentPoint.y)].isWall)
                        continue;
                }
                float tempG = current.g + Distance(neighbor.ToVector(),current.ToVector());
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
                    openSet.Add(neighbor);
                    neighbor.g = tempG;  
                    neighbor.h = Distance(neighbor.ToVector(), end.ToVector());
                    neighbor.previous = current;
                }
            }
            if (openSet.Count == 0)
            {
                path.Add(current);
                Node parent = current.previous;
                while (parent != null)
                {
                    path.Add(parent);
                    parent = parent.previous;
                }
                break;
            }
            
        }
        path.Reverse();
        List<Vector2> vectorPath = new List<Vector2>();
        foreach (Node node in path)
        {
            vectorPath.Add(node.ToVector());
        }
        Path result = new Path(vectorPath);
        if (DebugToggler.pathShown)
            Debug.Log(result);
        return result;
    }

    static float Distance(Vector2 a, Vector2 b)
    {
        return (Mathf.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y)));
        //mf Pythagoras going hard
    }

    /// <summary>
    /// in context, gets the bool grid from a method in the roomManager
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="roomManager"></param>
    /// <returns></returns>
    static Node[][] ToNodeGrid(bool[][] grid, RoomManager roomManager)
    {
        Node[][] result = new Node[grid.Length][];
        for (int i = 0; i < result.Length; i++)
            result[i] = new Node[grid[0].Length];
        for (int i = 0; i < result.Length; i++)
            for (int j = 0; j < result[0].Length; j++)
            {
                result[i][j] = new Node(roomManager.GridToWorld(i, j));
                result[i][j].isWall = !(grid[i][j]);
            }
        return result;
    }
}

class Node
{
    public float x;
    public float y; //actual coordinates in space, not "grid" coordinates
    public float g = 100000;
    public float h = 100000;
    //set to some insanely high number until it is "explored" by algorithm
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

    override
    public string ToString()
    {
        return ToVector().ToString();
    }
    public Vector2 ToVector()
    {
        return new Vector2(x, y);
    }
    /// <summary>
    /// looks at neighboring Nodes in the grid
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="gridPoint"></param>
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
