using UnityEngine;
using DG.Tweening;
using System;
using amed.utils.sound;
using amed.utils.serviceLoc;

public class FinishTile : Tile
{
    [SerializeField] AudioClip _playerWinSFX;

    public override void React(Player player)
    {
        player.transform.DOMove(transform.position, player.Speed).
        SetSpeedBased().
        SetEase(Ease.Linear).
        OnComplete(CallToWinGame);
    }

    void CallToWinGame()
    {
        ServiceLocator.Instance.Get<SoundManager>().PlaySound(_playerWinSFX);
    }
}
