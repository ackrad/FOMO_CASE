using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // serialized for quick testing
    private GridManager gridManager;

    // Can read from resources but good enough for now
    [SerializeField] private List<TextAsset> gridJsons;
    
    private void Start()
    {
        gridManager = GetComponentInChildren<GridManager>();
        ActionManager.OnNewLevelLoaded += LoadLevel;
    }

    private void LoadLevel(int level)
    {
        TextAsset gridJson = gridJsons[(level - 1) % gridJsons.Count];
        GridData gridData = ReadJson(gridJson);
        if (gridData == null)
        {
            Debug.LogError("GridData is null");
            return;
        }
        // set camera position magic numbers babbbby
        Camera.main.transform.position = new Vector3((gridData.ColCount-1)*0.5f ,(gridData.ColCount*gridData.RowCount)/12 + (11/3) , -(gridData.RowCount / 2f));
        gridManager.StartLevel(gridData);
        GameManager.Instance.SetMoveCount(gridData.MoveLimit);
    }
    
    
    private GridData ReadJson(TextAsset gridJson)
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
