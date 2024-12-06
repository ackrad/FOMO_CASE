using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using NaughtyAttributes;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private GridObjectsData gridObjectsData;
    [SerializeField] private GameObject gridBgPrefab;
    [SerializeField] private ExitDoor exitDoorPrefab;
    
    [SerializeField] private Transform gridBGParent;
    [SerializeField] private Transform gridObjectsParent;
    [SerializeField] private Transform exitDoorsParent;
    
   public GridUnit[,] InitializeGrid(GridData gridData)
    {
        
        ClearLevel();
        
        // plus two because exits are inside grid in this code.
        Vector2Int gridSize = new Vector2Int(gridData.ColCount, gridData.RowCount);
        var grid = new GridUnit[gridSize.x, gridSize.y];
        
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                grid[x, y] = new GridUnit(new GridCoord(x, y), GridUnitType.Empty);
                var obj = LeanPool.Spawn(gridBgPrefab, gridBGParent, true);
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
            GridObject gridObject = InstantiateGridObject(length);
            gridObject.transform.position = startPosition.ToWorldPos();
            for (int i = 0; i < length; i++)
            {
                GridCoord currentPosition = startPosition + direction * i;
                grid[currentPosition.X, currentPosition.Y].SetGridUnitType(GridUnitType.Taken);
                grid[currentPosition.X, currentPosition.Y].gridObjectOnTop = gridObject;
            }
            
            // set grid rotation default is right
            gridObject.transform.rotation = Quaternion.Euler(0, Mathf.Abs(90 * (directions[0]-3))%180, 0);
            gridObject.SetMaterial(gridObjectsData.GetMaterialByColorInt(movables.Colors, length), movables.Colors);
            foreach (var directionAsInt in directions)
            {
                gridObject.AddToMovableDirections(directionAsInt);
            }
            
            
        }


        foreach (var exitInfo in gridData.ExitInfo)
        {
            // get grid pos 
            GridCoord coord = new GridCoord(exitInfo.Col, exitInfo.Row);
            var exitObj = LeanPool.Spawn(exitDoorPrefab, exitDoorsParent);
            exitObj.transform.position = (coord).ToWorldPos() + exitInfo.Direction.TurnToDirection().ToWorldPos()*0.5f;
            exitObj.transform.rotation = Quaternion.Euler(0, 90 * (exitInfo.Direction), 0);
            
          
            exitObj.SetColors(gridObjectsData.GetColorByColorInt(exitInfo.Colors), exitInfo.Colors);
            exitObj.SetDirection(exitInfo.Direction.TurnToDirection());
            grid[coord.X, coord.Y].AddAttachedExit(exitObj);

        }
        
        return grid;
    }
   
    private GridObject InstantiateGridObject(  int length)
    {
        var objToInstantiate = gridObjectsData.GetGridObjectBySize(length);
        if (objToInstantiate == null)
        {
            Debug.LogError("No object found with size: " + length);
            return null;
        }
        
        GridObject gridObject = LeanPool.Spawn(objToInstantiate, gridObjectsParent);
        
        return gridObject;
    }

    
    [Button]
    private void ClearLevel()
    {
        List<Transform> childrenList = new List<Transform>();

        //Adds all children to the list
        foreach (Transform childTrans in gridBGParent)
            childrenList.Add(childTrans);

        foreach (Transform childTrans in gridObjectsParent)
        {
            childrenList.Add(childTrans);
        }

        foreach (Transform childTrans in exitDoorsParent)
        {
            childrenList.Add(childTrans);
        }


        foreach (var child in childrenList)
        {
            LeanPool.Despawn(child.gameObject);
        }
    }
    
    
}
