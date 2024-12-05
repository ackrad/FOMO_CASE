using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions 
{
    public static GridCoord ToGridCoord(this Vector2 vector2)
    {
        return new GridCoord(Mathf.RoundToInt(vector2.x), Mathf.RoundToInt(vector2.y));
    }
    
    public static GridCoord ToGridCoord(this Vector3 vector3)
    {
        return new GridCoord(Mathf.RoundToInt(vector3.x), Mathf.RoundToInt(vector3.z));
    }
    
    public static Vector3 ToWorldPos(this GridCoord gridCoord)
    {
        return new Vector3(gridCoord.X, 0, -gridCoord.Y);
    }


    public static GridCoord TurnToDirection(this int direction)
    {
        switch (direction)
        {
            case 0:
                return GridCoord.Up ;
            case 1:
                return GridCoord.Right;
            case 2:
                return GridCoord.Down;
            case 3:
                return GridCoord.Left;
            default:
                return GridCoord.Up;
        }
    }

    public static GridCoord TurnToDirectionLevelGeneration(this int direction)
    {
        switch (direction)
        {
            case 0:
                return GridCoord.Down ;
            case 1:
                return GridCoord.Right;
            case 2:
                return GridCoord.Down;
            case 3:
                return GridCoord.Right;
            default:
                return GridCoord.Up;
        }
        
        
        
    }

}
