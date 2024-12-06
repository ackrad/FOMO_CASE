using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{

    [SerializeField] private Transform leftDoorPivot;
    [SerializeField] private MeshRenderer leftDoorMesh;
    
    [SerializeField] private Transform rightDoorPivot;
    [SerializeField] private MeshRenderer rightDoorMesh;
    
    
    private GridCoord direction;
    private int exitColor;
    
    
    public void SetColors(Color color1,int colorInt)
    {
        SetColor(leftDoorMesh, color1);
        SetColor(rightDoorMesh, color1);
        exitColor = colorInt;
        
    }


    public void SetDirection(GridCoord exitDirection)
    {
        direction = exitDirection;
    }
    
    private void SetColor(MeshRenderer meshRenderer, Color color)
    {
        meshRenderer.material.color = color;
    }
  
    public GridCoord GetDirection()
    {
        return direction;
    }
    
    
    public bool CheckIfCanPlayerExitColor(int colorInt)
    {
        return colorInt == exitColor;
    }
    
    public void OpenDoor()
    {
        leftDoorPivot.DOPunchRotation(new Vector3(0, -120, 0), 2f,1).SetEase(Ease.OutBounce);
        rightDoorPivot.DOPunchRotation(new Vector3(0, 120, 0), 2f,1).SetEase(Ease.OutBounce);
    }
    
}
