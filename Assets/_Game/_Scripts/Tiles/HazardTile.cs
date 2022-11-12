using UnityEngine;
using DG.Tweening;
using amed.utils.sound;
using amed.utils.serviceLoc;

public class HazardTile : Tile
{
    public override void React(Player player)
    {
        player.transform.DOMove(transform.position, player.Speed).
        SetSpeedBased().
        SetEase(Ease.Linear);
        _playerEnteredFb.PlayFeedbacks();
        ServiceLocator.Instance.Get<GameManager>().PlayerDied();
    }
}
