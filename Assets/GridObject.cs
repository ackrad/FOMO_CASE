using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GridObject : MonoBehaviour
{
    [SerializeField] private Vector2Int objectSize;
    // use this bool for object rotation
    private bool isObjectHorizontal = true;
    
    private GridCoord objectStartPos;
    private GridCoord objectEndPos;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetObjectRotation(bool isHorizontal)
    {
        isObjectHorizontal = isHorizontal;
    }
}
