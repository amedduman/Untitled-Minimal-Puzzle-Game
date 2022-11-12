using System;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using MoreMountains.Feedbacks;

[SelectionBase]
public abstract class Tile : MonoBehaviour
{
    [HideInInspector] public int tileIdX;
    [HideInInspector] public int tileIdY;

    [ReadOnly] public Tile rightNeighbor;
    [ReadOnly] public Tile leftNeighbor;
    [ReadOnly] public Tile upNeighbor;
    [ReadOnly] public Tile downNeighbor;

    public MMF_Player _playerEnteredFb;

    LayerMask _layerToRaycast;

    void Awake()
    {
        // to prevent raycast detect object's collider.
        Physics2D.queriesStartInColliders = false;

        _layerToRaycast = LayerMask.GetMask("Tile");
    }

    public virtual void React(Player player)
    {
        _playerEnteredFb.PlayFeedbacks();   

        player.transform.DOMove(transform.position, player.Speed).
        SetSpeedBased().
        SetEase(Ease.Linear).
        OnComplete(player.Move);
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
        // Debug.DrawRay(transform.position, dir, Color.red, 5);
        if (hit.collider != null)
        {
            return hit.collider.transform.parent.gameObject.GetComponent<Tile>();    
        }
        return null;
    }
}