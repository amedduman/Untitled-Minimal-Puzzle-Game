using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Sirenix.OdinInspector;

[SelectionBase]
public class Tile : MonoBehaviour
{
    public TileType _tileType = TileType.Empty;
    public SpriteRenderer TileSpriteRenderer;
    [HideInInspector] public int tileIdX;
    [HideInInspector] public int tileIdY;

    [HideInInspector] public Tile rightNeighbor;
    [HideInInspector] public Tile leftNeighbor;
    [HideInInspector] public Tile upNeighbor;
    [HideInInspector] public Tile downNeighbor;

    [ReadOnly] [SerializeField] TileFeature _tileFeature = null; 

    #if UNITY_EDITOR
    [Button] 
    void ChangeType()
    {
        switch (_tileType)
        {
            case TileType.Empty:
                if (_tileFeature != null)
                {
                    DestroyImmediate(_tileFeature);
                    _tileFeature = null;
                }
                break;
            case TileType.TurnPoint: 
                if (_tileFeature != null)
                {
                    DestroyImmediate(_tileFeature);
                    _tileFeature = AddComponentViaEditor<TurnPoint>();
                }
                else
                { 
                    _tileFeature = AddComponentViaEditor<TurnPoint>(); 
                }
                break;
            default:
                throw new System.NotImplementedException();
        }
    }

    T AddComponentViaEditor<T>() where T : TileFeature
    {
        // with gameObject.AddComponent() the presets will not work.
        return ObjectFactory.AddComponent<T>(gameObject);  
    }
    #endif

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

    public enum TileType
    {
        Empty,
        TurnPoint,
    }
}