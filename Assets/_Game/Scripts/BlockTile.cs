using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlockTile : Tile
{
    public override Tweener React(Player player, Action funcToCall)
    {
        // base.React(player, funcToCall);

        Vector3 rot = player.transform.rotation.eulerAngles;
        rot.z -= 180;
        return player.transform.DORotate(rot, .3f).OnComplete(()=>funcToCall());

    }
}
