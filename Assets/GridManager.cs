using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lean.Pool;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    [SerializeField] private Vector2Int gridSize;
    
    private GridUnit[,] grid;
    GridGenerator gridGenerator;
    
    // open json in inspector for easy access adn testing
    
    private void Start()
    {
        gridGenerator = GetComponent<GridGenerator>();
    }
    
    public void StartLevel(GridData gridData)
    {
        grid =gridGenerator.InitializeGrid(gridData);
        gridSize = new Vector2Int(gridData.ColCount, gridData.RowCount);
    }
    
    public void ResolvePlayerInput(Vector3 worldStartPos, GridCoord gridCoordChange)
    {
        GridCoord gridStartCoord = worldStartPos.ToGridCoord();
        
        // check if out of bounds
        if (!IsOnGrid(gridStartCoord)) return;
        
        GridObject gridObject = grid[gridStartCoord.X, gridStartCoord.Y].gridObjectOnTop;
        
        // if there is no object to move
        if(gridObject == null)
        {
            Debug.Log("No object found at: " + gridStartCoord.X + " " + gridStartCoord.Y);
            return;
        }
        
        // check if gridobject can move in direction
        if(!gridObject.CheckIfCanMoveInDirection(gridCoordChange.TurnToDirection()))
        {
            Debug.Log("Can't move in that direction");
            return;
        }
        
        
        gridStartCoord = gridObject.transform.position.ToGridCoord();
        // assume object are placed from left to right top to bottom
        // if moving direction is right or down check ahead
        int checkAheadCount = 0;
        int objectLength = gridObject.GetObjectSize();
        GridCoord objectDirection = gridObject.ReturnObjectOrientation();
        if (gridCoordChange == objectDirection)
        {
            checkAheadCount = objectLength-1;
        }
        
        // move as much as possible
        int i = 0;
        int loopMax = 100;
        GridCoord endingPoint = gridStartCoord;
        GridObject hitGridObject = null;
        while (true)
        {
           GridCoord newGridCoord = gridStartCoord + gridCoordChange * (i);

            // check if out of bounds
            if (!IsOnGrid(newGridCoord)) break;
            
            // check if the space is empty
            if (grid[newGridCoord.X, newGridCoord.Y].GridUnitType == GridUnitType.Taken &&
              grid[newGridCoord.X, newGridCoord.Y].gridObjectOnTop != gridObject)
            {
                hitGridObject = grid[newGridCoord.X, newGridCoord.Y].gridObjectOnTop;
                break;
            }
            
            //set new ending point
            endingPoint = newGridCoord;
            i++;
            
            // protection from infinite loop
            if (i > loopMax)
            {
                Debug.LogError("Loop max reached");
                break;
            }
        }
        
        if(endingPoint == gridStartCoord)
        {
            gridObject.CantMoveShake();
            return;
        }
        
        
        // check if ends near exit and if it can exit
        bool doesExit = false;
        if (grid[endingPoint.X, endingPoint.Y].CheckIfCanPlayerExit(gridCoordChange, gridObject.GetColorInt()))
        {
            grid[endingPoint.X, endingPoint.Y].OpenDoor(gridCoordChange);
            endingPoint = endingPoint + gridCoordChange*(i + objectLength-1);
            doesExit = true;
        }
        
        // move the object
        if (doesExit)
        {
            gridObject.transform.SetParent(null);
            gridObject.DespawningRoutine(endingPoint.ToWorldPos(),gridCoordChange);
        }
        else
        {
            endingPoint = endingPoint - gridCoordChange*checkAheadCount;
            gridObject.transform.DOMove(endingPoint.ToWorldPos(), 0.4f).SetEase(Ease.OutBounce);
            if(hitGridObject != null)
            {
                hitGridObject.ObjectHitFromDirection(gridCoordChange);
            }
        }
        
        // empty starting point with length
        for (int j = 0; j < objectLength; j++)
        {
            
            GridCoord currentGridCoord = gridStartCoord + objectDirection * j;
            Debug.Log("Emptying: " + currentGridCoord.X + " " + currentGridCoord.Y);
            grid[currentGridCoord.X, currentGridCoord.Y].SetGridUnitType(GridUnitType.Empty);
            grid[currentGridCoord.X, currentGridCoord.Y].gridObjectOnTop = null;
        }
        
        // fill ending point
        if (!doesExit)
        {
            for (int j = 0; j < objectLength; j++)
            {
                GridCoord currentGridCoord = endingPoint + objectDirection * j;
                grid[currentGridCoord.X, currentGridCoord.Y].SetGridUnitType(GridUnitType.Taken);
                grid[currentGridCoord.X, currentGridCoord.Y].gridObjectOnTop = gridObject;
            }
        }
        
        CheckForWinCondition();
        
        GameManager.Instance.DecreaseMoveCount();


    }
    
    
    private bool IsOnGrid(GridCoord gridCoord)
    {
        if (gridCoord.X < 0 || gridCoord.X >= gridSize.x || gridCoord.Y < 0 || gridCoord.Y >= gridSize.y)
        {
            return false;
        }
        return true;
    }
    
    private void CheckForWinCondition()
    {
        bool isWin = true;
        foreach (var gridUnit in grid)
        {
            if (gridUnit.GridUnitType == GridUnitType.Taken)
            {
                isWin = false;
                break;
            }
        }
        
        if (isWin)
        {
            GameManager.Instance.WinGame();
        }
    }
    
}



