using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIText : MonoBehaviour
{
    [SerializeField] private UIUpdateType uiUpdateType;
    [SerializeField] private TextMeshProUGUI levelText;

    private string prefix = "";
    // Start is called before the first frame update
    void Start()
    {
        switch (uiUpdateType)
        {
            case UIUpdateType.LevelCount:
                prefix = "Level: ";
                ActionManager.OnNewLevelLoaded += UpdateText;
                break;
            case UIUpdateType.MoveCount:
                prefix = "Moves: ";
                ActionManager.OnMoveCountUpdated += UpdateText;
                break;
        }
    }

    private void UpdateText(int value)
    {
        levelText.text = prefix + value;
        transform.DOPunchScale(Vector3.one * 0.1f, 0.5f);
    }
    
   
}
