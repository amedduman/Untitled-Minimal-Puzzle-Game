using System;
using UnityEngine;

[SelectionBase]
public abstract class Tile : MonoBehaviour
{
    [HideInInspector] public int tileIdX;
    [HideInInspector] public int tileIdY;

    public Tile rightNeighbor;
    public Tile leftNeighbor;
    public Tile upNeighbor;
    public Tile downNeighbor;

    void Awake()
    {
        // to prevent raycast detect object's collider.
        Physics2D.queriesStartInColliders = false;
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir);
        Debug.DrawRay(transform.position, dir, Color.red, 5);
        if (hit.collider != null)
        {
            return hit.collider.transform.parent.gameObject.GetComponent<Tile>();    
        }
        return null;
    }
}