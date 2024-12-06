using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


[CreateAssetMenu(fileName = "GridObjectsData", menuName = "ScriptableObjects/GridObjectsData", order = 1)]
public class GridObjectsData : ScriptableObject
{
    public List<GridObject> GridObjects;
    
    public List<ColorMaterialMatch> ColorMaterialMatches;
    public GridObject GetGridObject(int index)
    {
        return GridObjects[index];
    }
    
    public Material GetMaterialByColorInt(int color,int length)
    {
        foreach (var colorMaterialMatch in ColorMaterialMatches)
        {
            if (colorMaterialMatch.ColorInt == color)
            {
                foreach (var materialMatch in colorMaterialMatch.MaterialMatches)
                {
                    if (materialMatch.Length == length)
                    {
                        return materialMatch.Material[Random.Range(0, materialMatch.Material.Count)];
                    }
                }
            }
        }
        return null;
    }
    
    public Color GetColorByColorInt(int color)
    {
        foreach (var colorMaterialMatch in ColorMaterialMatches)
        {
            if (colorMaterialMatch.ColorInt == color)
            {
                return colorMaterialMatch.Color;
            }
        }
        return Color.white;
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


[Serializable]
public class ColorMaterialMatch
{
    public int ColorInt;
    public Color Color;
    public List<MaterialMatch> MaterialMatches;
}

[Serializable]
public class MaterialMatch{
    public List<Material> Material;
    public int Length;
    
    
}