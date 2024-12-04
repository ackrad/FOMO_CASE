using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private float cellSize =1f;
    
    private GridUnit[,] grid;
    
    
    
    public void InitializeGrid()
    {
        grid = new GridUnit[gridSize.x, gridSize.y];
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                grid[x, y] = new GridUnit(new GridCoord(x, y), this, GetGridUnitTypeByCoord(new GridCoord(x, y)));
            }
        }
        
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
