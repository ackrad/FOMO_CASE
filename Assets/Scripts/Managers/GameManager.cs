using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    private int moveCount = 0;
    private int currentLevel = 0;

    public bool DoesHaveInfinityMoves
    {
        private set;
        get;
    } = false;
    
    public bool IsGameStarted { get; private set; } = false;
    
    [SerializeField] private int testStartLevel = 0;
    
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
    
    private IEnumerator Start()
    {
        if (testStartLevel > 0)
        {
            currentLevel = testStartLevel;
        }
        yield return null;
        LoadNextLevel();
    }

    public void SetMoveCount(int moveCount)
    {
        this.moveCount = moveCount;
        if (this.moveCount == 0)
        {
            DoesHaveInfinityMoves = true;
            ActionManager.OnInfiniteMoves?.Invoke();
        }
        IsGameStarted = true;
        ActionManager.OnMoveCountUpdated?.Invoke(this.moveCount);
    }
    
    public void DecreaseMoveCount()
    {
        if(DoesHaveInfinityMoves) return;
        
        moveCount--;
        ActionManager.OnMoveCountUpdated?.Invoke(moveCount);
        if (moveCount == 0)
        {
            LoseGame();
        }
    }
    
    public void LoadNextLevel()
    {
        DoesHaveInfinityMoves = false;
        currentLevel++;
        ActionManager.OnNewLevelLoaded?.Invoke(currentLevel);
    }
    
    
    
    
    public void RestartLevel()
    {
        DoesHaveInfinityMoves = false;
        ActionManager.OnNewLevelLoaded?.Invoke(currentLevel);

    }

    public void WinGame()
    {
        if (!IsGameStarted) return;
        IsGameStarted = false;
        ActionManager.OnGameWin?.Invoke();
    }
    
    private void LoseGame()
    {
        if (!IsGameStarted) return;
        IsGameStarted = false;
        ActionManager.OnGameLose?.Invoke();
    }
    
}
