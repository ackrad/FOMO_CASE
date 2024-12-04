using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "GridObjectsData", menuName = "ScriptableObjects/GridObjectsData", order = 1)]
public class GridObjectsData : ScriptableObject
{
    public List<GridObject> GridObjects;
    
    
    public GridObject GetGridObject(int index)
    {
        return GridObjects[index];
    }
    
    //return gridObjectBySize
    public GridObject GetGridObjectBySize(int size)
    {
        foreach (var gridObject in GridObjects)
        {
            if (gridObject.GetObjectSize() == size)
            {
                return gridObject;
            }
        }
        return null;
    }
    
    
}

