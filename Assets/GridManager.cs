using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private float cellSize =1f;
    
    private GridUnit[,] grid;
    // open json in inspector for easy access adn testing
    [SerializeField] private GridObjectsData gridObjectsData;
    [SerializeField] private GameObject gridBgPrefab;
    [SerializeField] private ExitDoor exitDoorPrefab;
    public void InitializeGrid(GridData gridData)
    {
       // plus two because exits are inside grid in this code.
        gridSize = new Vector2Int(gridData.ColCount, gridData.RowCount);
        grid = new GridUnit[gridSize.x, gridSize.y];
        
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                grid[x, y] = new GridUnit(new GridCoord(x, y), this, GridUnitType.Empty);
                // create bg if its not on the edge
                
                var obj = Instantiate(gridBgPrefab);
                obj.transform.position = new GridCoord(x, y).ToWorldPos();
            }
        }
        
        
        foreach (var movables in gridData.MovableInfo)
        {
            GridCoord startPosition = new GridCoord(movables.Col, movables.Row);
            List<int> directions = movables.Direction;
            
            // objects are always placed from left to right top to bottom.
            GridCoord direction = directions[0].TurnToDirectionLevelGeneration();
            int length = movables.Length;
            GridObject gridObject = InstantiateGridObject(startPosition, length);
            gridObject.transform.position = startPosition.ToWorldPos();
            grid[startPosition.X, startPosition.Y].SetGridUnitType(GridUnitType.Taken);
            for (int i = 0; i < length; i++)
            {
                GridCoord currentPosition = startPosition + direction * i;
                grid[currentPosition.X, currentPosition.Y].SetGridUnitType(GridUnitType.Taken);
            }
            
            // set grid rotation default is right
            gridObject.transform.rotation = Quaternion.Euler(0, 90 * (directions[0]-3), 0);
            
        }


        foreach (var exitInfo in gridData.ExitInfo)
        {
            // get grid pos 
            GridCoord coord = new GridCoord(exitInfo.Col, exitInfo.Row);
            var exitObj = Instantiate(exitDoorPrefab);
            exitObj.transform.position = (coord).ToWorldPos() + exitInfo.Direction.TurnToDirection().ToWorldPos()*0.5f;
            exitObj.transform.rotation = Quaternion.Euler(0, 90 * (exitInfo.Direction), 0);


        }
        
    }
    private GridObject InstantiateGridObject(GridCoord gridCoord,  int length)
    {
        var objToInstantiate = gridObjectsData.GetGridObjectBySize(length);
        if (objToInstantiate == null)
        {
            Debug.LogError("No object found with size: " + length);
            return null;
        }
        
        GridObject gridObject = Instantiate(objToInstantiate, transform);
        
        return gridObject;
    }
    
    
    private GridUnitType GetGridUnitTypeByCoord(GridCoord gridCoord)
    {
        if (gridCoord.X < 0 || gridCoord.X >= gridSize.x || gridCoord.Y < 0 || gridCoord.Y >= gridSize.y)
        {
            return GridUnitType.OutOfBounds;
        }
       
        // if its on the edge its a wall or an exit
        if (gridCoord.X == 0 || gridCoord.X == gridSize.x - 1 || gridCoord.Y == 0 || gridCoord.Y == gridSize.y - 1)
        {
            return GridUnitType.Wall;
        }
        
        return GridUnitType.Empty;
    }
    

    
    
}



