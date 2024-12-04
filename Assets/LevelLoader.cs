using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    // serialized for quick testing
    [SerializeField] private TextAsset gridJson;
    private GridManager gridManager;


    private void Start()
    {
        gridManager = GetComponentInChildren<GridManager>();
        
    }

    [Button]
    private void LoadLevel()
    {
        GridData gridData = ReadJson();
        if (gridData == null)
        {
            Debug.LogError("GridData is null");
            return;
        }
        
        
        gridManager.InitializeGrid(gridData);
    }
    
    
    private GridData ReadJson()
    {
        if (gridJson == null)
        {
            return null;
        }
        
        fsSerializer fsSerializer = new fsSerializer();
        fsData data = fsJsonParser.Parse(gridJson.text);
        object deserialized = null;
        fsSerializer.TryDeserialize(data, typeof(GridData), ref deserialized).AssertSuccessWithoutWarnings();
        GridData gridData = (GridData) deserialized;
        
        return gridData;
    }
}
