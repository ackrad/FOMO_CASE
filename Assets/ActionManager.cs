using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActionManager 
{
    public static Action<int> OnNewLevelLoaded;
    public static Action<int> OnMoveCountUpdated;
    public static Action OnGameWin;
    public static Action OnGameLose;
    public static Action OnInfiniteMoves;

}
