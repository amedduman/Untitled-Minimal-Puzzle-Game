using UnityEngine;
using DG.Tweening;
using System;
using amed.utils.sound;
using amed.utils.serviceLoc;

public class FinishTile : Tile
{
    public override void React(Player player)
    {
        player.transform.DOMove(transform.position, player.Speed).
        SetSpeedBased().
        SetEase(Ease.Linear).
        OnComplete(CallToWinGame);
    }

    void CallToWinGame()
    {
        _playerEnteredFb.PlayFeedbacks();
    }
}
