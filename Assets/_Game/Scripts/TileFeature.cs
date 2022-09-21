using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tile))]
public abstract class TileFeature : MonoBehaviour
{
    protected Tile MyTile {get; private set;}

    public virtual void Reset()
    {
        MyTile = GetComponent<Tile>();
    }
}
