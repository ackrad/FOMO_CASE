using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    private int _moveCount = 0;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    
    public void SetMoveCount(int moveCount)
    {
        _moveCount = moveCount;
        ActionManager.OnMoveCountUpdated?.Invoke(_moveCount);
    }
    
    public void DecreaseMoveCount()
    {
        _moveCount--;
        ActionManager.OnMoveCountUpdated?.Invoke(_moveCount);
    }
    
}
