using UnityEngine;
using DG.Tweening;
using amed.utils.sound;

public class HazardTile : Tile
{
    public override void React(Player player)
    {
        player.transform.DOMove(transform.position, player.Speed).
        SetSpeedBased().
        SetEase(Ease.Linear);
        _playerEnteredFb.PlayFeedbacks();
        ServiceLocator.Get<GameController>().PlayerDied();
    }
}
