using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GridObject : MonoBehaviour
{
    [SerializeField] private Vector2Int objectSize;
    
    // uses the same values as int in the provided document.
    private List<int> movableDirections = new List<int>();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void AddToMovableDirections(int direction)
    {
        if(direction is >= 0 and <= 3 && !movableDirections.Contains(direction))
        {
            
            movableDirections.Add(direction);
        }
        
    }
}
