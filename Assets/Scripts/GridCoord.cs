using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCoord 
{
    public int X;
    public int Y;
    public Vector3 Position => new Vector3(X, 0, Y);
    
    public static GridCoord Up = new GridCoord(0, 1);
    public static GridCoord Down = new GridCoord(0, -1);
    public static GridCoord Left = new GridCoord(-1, 0);
    public static GridCoord Right = new GridCoord(1, 0);
    
    
    public GridCoord(int x, int y)
    {
        X = x;
        Y = y;
    }
    
   
    public override int GetHashCode()
    {
        return X ^ Y;
    }
    
    public static GridCoord operator +(GridCoord a, GridCoord b)
    {
        return new GridCoord(a.X + b.X, a.Y + b.Y);
    }

    public static GridCoord operator -(GridCoord a, GridCoord b)
    {
        return new GridCoord(a.X - b.X, a.Y - b.Y);
    }

    public static bool operator ==(GridCoord a, GridCoord b)
    {
        // null check
        if (ReferenceEquals(a, null))
        {
            return false;
        }
        if (ReferenceEquals(b, null))
        {
            return false;
        }
        return a.X == b.X && a.Y == b.Y;
    }

    public static bool operator !=(GridCoord a, GridCoord b)
    {
        // null check
        if (ReferenceEquals(a, null))
        {
            return false;
        }
        if (ReferenceEquals(b, null))
        {
            return false;
        }
        return a.X != b.X || a.Y != b.Y;
    }

    public static GridCoord operator *(GridCoord a,int b)
    {
        return new GridCoord(a.X * b, a.Y * b);
    }

    public static GridCoord operator -(int a, GridCoord b)
    {
        return new GridCoord(b.X * a, b.Y * a);
    }
    
}
