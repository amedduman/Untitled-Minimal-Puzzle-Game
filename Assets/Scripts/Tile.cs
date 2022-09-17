using System;
using UnityEngine;

[SelectionBase]
public class Tile : MonoBehaviour
{
    public SpriteRenderer TileSpriteRenderer;
    [HideInInspector] public int tileIdX;
    [HideInInspector] public int tileIdY;
   
    [HideInInspector] public Tile rightNeighbor;
    [HideInInspector] public Tile leftNeighbor;
    [HideInInspector] public Tile upNeighbor;
    [HideInInspector] public Tile downNeighbor;
    

    public void SetNeighbors(Tile[,] tiles)
    {
        // right neighbor
        try
        {
            rightNeighbor = tiles[tileIdX + 1, tileIdY];
        }
        catch (IndexOutOfRangeException)
        {
            rightNeighbor = null;
        }
        
        // left neighbor
        try
        {
            leftNeighbor = tiles[tileIdX - 1, tileIdY];
        }
        catch (IndexOutOfRangeException)
        {
            leftNeighbor = null;
        }
        
        // up neighbor
        try
        {
            upNeighbor = tiles[tileIdX, tileIdY + 1];
        }
        catch (IndexOutOfRangeException)
        {
            upNeighbor = null;
        }
        
        // down neighbor
        try
        {
            downNeighbor = tiles[tileIdX, tileIdY - 1];
        }
        catch (IndexOutOfRangeException)
        {
            downNeighbor = null;
        }
    }
}