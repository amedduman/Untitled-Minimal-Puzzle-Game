using System;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

[SelectionBase]
public abstract class Tile : MonoBehaviour
{
    [SerializeField] LayerMask _layerToRaycast;

    [HideInInspector] public int tileIdX;
    [HideInInspector] public int tileIdY;

    [ReadOnly] public Tile rightNeighbor;
    [ReadOnly] public Tile leftNeighbor;
    [ReadOnly] public Tile upNeighbor;
    [ReadOnly] public Tile downNeighbor;

    void Awake()
    {
        // to prevent raycast detect object's collider.
        Physics2D.queriesStartInColliders = false;
    }

    public virtual Tweener React(Player player, Action funcToCall)
    {
        return player.transform.DOMove(transform.position, player.Speed).SetSpeedBased().SetEase(Ease.Linear).OnComplete(()=>funcToCall());
    }

    public void SetNeighborsNewMethod()
    {
        upNeighbor = RaycastToNeighbor(Vector2.up);
        downNeighbor = RaycastToNeighbor(Vector2.down);
        rightNeighbor = RaycastToNeighbor(Vector2.right);
        leftNeighbor = RaycastToNeighbor(Vector2.left);
    }

    Tile RaycastToNeighbor(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1, _layerToRaycast);
        Debug.DrawRay(transform.position, dir, Color.red, 5);
        if (hit.collider != null)
        {
            return hit.collider.transform.parent.gameObject.GetComponent<Tile>();    
        }
        return null;
    }
}