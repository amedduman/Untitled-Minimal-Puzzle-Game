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

     public void SetNeighborsNewMethod()
     {
         // raycast to find out neighbours...
     }
     
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