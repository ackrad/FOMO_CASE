using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GridObject : MonoBehaviour
{
    [SerializeField] private int objectSize;
    
    // uses the same values as int in the provided document.
    private List<int> movableDirections = new List<int>();


    private int colorInt;
   
    
    
    public void AddToMovableDirections(int direction)
    {
        if(direction is >= 0 and <= 3 && !movableDirections.Contains(direction))
        {
            
            movableDirections.Add(direction);
        }
        
    }
    
    
    public bool CheckIfCanMoveInDirection(int direction)
    {
        return movableDirections.Contains(direction);
    }
    
    public void SetMaterial(Material material, int color)
    {
        colorInt = color;
        GetComponentInChildren<MeshRenderer>().material = material;
    }
    
    public int GetColorInt()
    {
        return colorInt;
    }
    
    public int GetObjectSize()
    {
        return objectSize;
    }
    
    public GridCoord ReturnObjectOrientation()
    {
        return movableDirections[0].TurnToDirectionLevelGeneration();
    }
}
