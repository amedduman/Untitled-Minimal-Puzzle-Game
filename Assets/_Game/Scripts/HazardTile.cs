using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class HazardTile : Tile
{
    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if(other.gameObject.TryGetComponent(out Player player))
    //     {
    //         player.Stop();
    //         player.transform.DOMove(transform.position, 2).SetSpeedBased().SetEase(Ease.Linear);
    //     }
    // }

    public override Tweener React(Player player, Action funcToCall)
    {
        funcToCall = CallToLoseGame;
        return base.React(player, funcToCall);
    }

    void CallToLoseGame()
    {
        Debug.Log("Lose");
    }
}
