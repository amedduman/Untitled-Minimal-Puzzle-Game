using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlockTile : Tile
{
    public override void React(Player player)
    {
        // base.React(player, funcToCall);

        Vector3 rot = player.transform.rotation.eulerAngles;
        rot.z -= 180;
        player.transform.DORotate(rot, .3f).OnComplete(player.Move);
    }
}
