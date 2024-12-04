using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUnit 
{
    public GridCoord GridPosition { get; private set; }
    public GridManager GridManager { get; private set; }
    public bool IsFree { get; private set; } = true;

    public GridUnitType GridUnitType { get; private set; } = GridUnitType.Empty;

    public void SetIsFree(bool isFree)
    {
        IsFree = isFree;
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
       IsFree = true;
       GridUnitType = gridUnitType;
   }
   
}
