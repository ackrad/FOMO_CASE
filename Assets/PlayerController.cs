using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Start()
    {
        LeanTouch.OnFingerSwipe += FingerSwiped;
    }

    private void FingerSwiped(LeanFinger finger)
    {
        // Get the object at swipe start location.
        
        // check the swipe direction
        
        // try and throw the block in the spesified direction
        
        // handle collision and exit animations depending on what happened
        
        
        
    }
}
