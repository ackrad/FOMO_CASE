using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUnit 
{
    public GridCoord GridPosition { get; private set; }

    private List<ExitDoor> attachedExits = new List<ExitDoor>();
    
    
    public GridUnitType GridUnitType { get; private set; } = GridUnitType.Empty;
    
    private bool isOnEdge = false;
    public GridObject gridObjectOnTop;

    public bool IsFreeToMoveThrough()
    {
        if(GridUnitType == GridUnitType.Empty)
        {
            return true;
        }
        return false;
    }
    
    public void SetGridUnitType(GridUnitType gridUnitType)
    {
        GridUnitType = gridUnitType;
    }
    
   //constructor
   public GridUnit(GridCoord gridPosition, GridUnitType gridUnitType)
   {
       GridPosition = gridPosition;
       GridUnitType = gridUnitType;
   }

   public void AddAttachedExit(ExitDoor exitDoor)
   {
       attachedExits.Add(exitDoor);
   }
   
    public void SetIsOnEdge(bool value)
    {
         isOnEdge = value;
    }
    
    public bool GetIsOnEdge()
    {
        return isOnEdge;
    }
    
    public bool CheckIfCanPlayerExit(GridCoord direction,int colorInt)
    {
        foreach (var exit in attachedExits)
        {
            if (exit.GetDirection() == direction && exit.CheckIfCanPlayerExitColor(colorInt))
            {
                return true;
            }
        }

        return false;
    }
    
    public void OpenDoor(GridCoord direction)
    {
        foreach (var exit in attachedExits)
        {
            if (exit.GetDirection() == direction)
            {
                exit.OpenDoor();
            }
        }
    }
   
}
