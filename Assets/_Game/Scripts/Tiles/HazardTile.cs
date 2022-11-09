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

    public override void React(Player player)
    {
        player.transform.DOMove(transform.position, player.Speed).
        SetSpeedBased().
        SetEase(Ease.Linear).
        OnComplete(CallToLoseGame);
    }

    void CallToLoseGame()
    {
        Debug.Log("Lose");
    }
}
