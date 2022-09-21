using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyTile : TileFeature
{
    [SerializeField] Sprite _sprite;

    public override void Reset()
    {
        base.Reset();

        if(_sprite != null)
            MyTile.TileSpriteRenderer.sprite = _sprite; 
    }
}
