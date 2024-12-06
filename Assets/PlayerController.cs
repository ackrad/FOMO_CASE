using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private GridManager gridManager;
    
    private void Start()
    {
        LeanTouch.OnFingerSwipe += FingerSwiped;
    }

    private void FingerSwiped(LeanFinger finger)
    {
        if (!GameManager.Instance.IsGameStarted) return;
        
        // Get the position at swipe start location.
        Vector3 worldStartPos = finger.GetStartWorldPosition(Camera.main.transform.position.y);
        
        Vector3 swipeDirection = finger.SwipeScaledDelta;
        
        // turn swipe into 4 direction
      
        GridCoord gridCoordChange = SwipeDeltaToGridCoordChange(swipeDirection);
        
        
        gridManager.ResolvePlayerInput(worldStartPos, gridCoordChange);
        
        
        
        
        
        
        
    }


    private GridCoord SwipeDeltaToGridCoordChange(Vector3 swipeDelta)
    {
        if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
        {
            if(swipeDelta.x > 0)
            {
                return GridCoord.Right;
            }
            else
            {
                return GridCoord.Left;
            }
        }
        else
        {
            if(swipeDelta.y > 0)
            {
                return GridCoord.Up;
            }
            else
            {
                return GridCoord.Down;
            }
        }
        
        
        
    }
}
