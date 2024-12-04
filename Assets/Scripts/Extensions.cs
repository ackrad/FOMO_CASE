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
    
    public static Vector3 ToVector3(this GridCoord gridCoord)
    {
        return new Vector3(gridCoord.X, 0, gridCoord.Y);
    }
    

}
