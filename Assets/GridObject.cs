using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;
using UnityEngine.Serialization;

public class GridObject : MonoBehaviour,IPoolable
{
    [SerializeField] private int objectSize;
    
    // uses the same values as int in the provided document.
    private List<int> movableDirections = new List<int>();

    private Rigidbody rb;

    private int colorInt;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    public void AddToMovableDirections(int direction)
    {
        if(direction is >= 0 and <= 3 && !movableDirections.Contains(direction))
        {
            
            movableDirections.Add(direction);
        }
        
    }
    
    
    public bool CheckIfCanMoveInDirection(int direction)
    {
        return movableDirections.Contains(direction);
    }
    
    public void SetMaterial(Material material, int color)
    {
        colorInt = color;
        GetComponentInChildren<MeshRenderer>().material = material;
    }
    
    public int GetColorInt()
    {
        return colorInt;
    }
    
    public int GetObjectSize()
    {
        return objectSize;
    }
    
    public GridCoord ReturnObjectOrientation()
    {
        return movableDirections[0].TurnToDirectionLevelGeneration();
    }
    
    public void ObjectHitFromDirection(GridCoord direction)
    {
       transform.DOPunchPosition(direction.ToVector3() * 0.1f, 0.2f).SetEase(Ease.OutQuint);
        
    }
    
    public void CantMoveShake()
    {
        transform.DOPunchRotation(Vector3.up * 10f, 0.6f).SetEase(Ease.OutQuint);
    }

    public void DespawningRoutine(Vector3 posToMove, GridCoord direction)
    {
        transform.DOMove(posToMove, 0.4f).SetEase(Ease.OutSine).OnComplete(() =>
        {
            DespawningRoutineContinue(direction);
        });
    }

    private void DespawningRoutineContinue(GridCoord direction)
    {
        rb.isKinematic = false;
        Vector3 directionVector = direction.ToVector3();
        rb.AddForce(directionVector * 1f, ForceMode.Impulse);
        transform.DOScale(Vector3.zero, 2f).SetEase(Ease.OutSine).OnComplete(() =>
        {
            LeanPool.Despawn(gameObject);
        });
    }
    
    public void OnDespawn()
    {
        movableDirections.Clear();
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
    }
    
    public void OnSpawn()
    {
        
    }
}
