using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUnit 
{
    public GridCoord GridPosition { get; private set; }
    public GridManager GridManager { get; private set; }

    public GridUnitType GridUnitType { get; private set; } = GridUnitType.Empty;

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
   public GridUnit(GridCoord gridPosition, GridManager gridManager, GridUnitType gridUnitType)
   {
       GridPosition = gridPosition;
       GridManager = gridManager;
       GridUnitType = gridUnitType;
   }
   
}
