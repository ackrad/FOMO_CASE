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
        if(gridObject == null) return;
        
        // check if gridobject can move in direction
        if (!gridObject.CheckIfCanMoveInDirection(gridCoordChange.TurnToDirection())) return;
        
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
        int movementAmount = TryAndMoveInDirection(gridCoordChange, gridStartCoord, gridObject, out var endingPoint, out var hitGridObject);

        // because  it checks the grid unit its on first == 1
        if(movementAmount == 1)
        {
            gridObject.CantMoveShake();
            return;
        }
        
        // check if ends near exit and if it can exit
        bool doesExit = false;
        if (grid[endingPoint.X, endingPoint.Y].CheckIfCanPlayerExit(gridCoordChange, gridObject.GetColorInt()))
        {
            grid[endingPoint.X, endingPoint.Y].OpenDoor(gridCoordChange);
            endingPoint = endingPoint + gridCoordChange*(movementAmount + objectLength-1);
            doesExit = true;
        }
        
        // move the object
        if (doesExit)
        {
            // throw the object off the cliff
            gridObject.transform.SetParent(null);
            gridObject.DespawningRoutine(endingPoint.ToWorldPos(),gridCoordChange);
        }
        else
        {
            // move to the end position and play effects
            endingPoint = MoveToPositionAndPlayEffects(gridCoordChange, gridObject, endingPoint, checkAheadCount, hitGridObject);
        }
        
        // empty starting point with length
        for (int j = 0; j < objectLength; j++)
        {
            TurnGridEmpty(gridStartCoord, objectDirection, j);
        }
        
        // fill ending point
        if (!doesExit)
        {
            for (int j = 0; j < objectLength; j++)
            {
                AssignGridObjectToGrid(endingPoint, objectDirection, j, gridObject);
            }
        }
        CheckForWinCondition();
        GameManager.Instance.DecreaseMoveCount();
    }

    private void AssignGridObjectToGrid(GridCoord endingPoint, GridCoord objectDirection, int j, GridObject gridObject)
    {
        GridCoord currentGridCoord = endingPoint + objectDirection * j;
        grid[currentGridCoord.X, currentGridCoord.Y].SetGridUnitType(GridUnitType.Taken);
        grid[currentGridCoord.X, currentGridCoord.Y].gridObjectOnTop = gridObject;
    }

    private void TurnGridEmpty(GridCoord gridStartCoord, GridCoord objectDirection, int j)
    {
        GridCoord currentGridCoord = gridStartCoord + objectDirection * j;
        grid[currentGridCoord.X, currentGridCoord.Y].SetGridUnitType(GridUnitType.Empty);
        grid[currentGridCoord.X, currentGridCoord.Y].gridObjectOnTop = null;
    }

    private static GridCoord MoveToPositionAndPlayEffects(GridCoord gridCoordChange, GridObject gridObject,
        GridCoord endingPoint, int checkAheadCount, GridObject hitGridObject)
    {
        gridObject.InstantiateHitParticleOnEdge(gridCoordChange, endingPoint);
        endingPoint -= gridCoordChange*checkAheadCount;
        gridObject.transform.DOMove(endingPoint.ToWorldPos(), 0.4f).SetEase(Ease.OutBounce);
        if(hitGridObject != null)
        {
            hitGridObject.ObjectHitFromDirection(gridCoordChange);
        }

        return endingPoint;
    }

    private int TryAndMoveInDirection(GridCoord gridCoordChange, GridCoord gridStartCoord, GridObject gridObject,
        out GridCoord endingPoint, out GridObject hitGridObject)
    {
        int i = 0;
        int loopMax = 100;
        endingPoint = gridStartCoord;
        hitGridObject = null;
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

        return i;
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



