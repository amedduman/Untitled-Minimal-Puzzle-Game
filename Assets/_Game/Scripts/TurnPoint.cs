using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPoint : TileFeature
{
    [SerializeField] Sprite _turnPointSprite;

    public override void Reset()
    {
        base.Reset();

        if(_turnPointSprite != null)
            MyTile.TileSpriteRenderer.sprite = _turnPointSprite;
        
        
    }
}
