using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WinLoseUI : MonoBehaviour
{
    
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    private void Start()
    {
        ActionManager.OnGameWin += ShowWinPanel;
        ActionManager.OnGameLose += ShowLosePanel;
        ActionManager.OnNewLevelLoaded += HidePanels;
        HidePanels(0);
    }

    
    private void ShowWinPanel()
    {
        winPanel.SetActive(true);
        PanelOpenAnimation(winPanel);
    }
    
    private void ShowLosePanel()
    {
        losePanel.SetActive(true);
        PanelOpenAnimation(losePanel);
    }
    
    private void HidePanels(int level)
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }
    
    private void PanelOpenAnimation(GameObject panel)
    {
        panel.transform.localScale = Vector3.zero;
        panel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutSine);
    }
    

    public void NextLevelButton()
    {
        GameManager.Instance.LoadNextLevel();
        
    }
    
    public void RestartLevelButton()
    {
        GameManager.Instance.RestartLevel();
        
    }
}
