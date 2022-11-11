using UnityEngine;
using DG.Tweening;
using amed.utils.sound;
using amed.utils.serviceLoc;

public class HazardTile : Tile
{
    [SerializeField] AudioClip _playerLoseSFX;

    public override void React(Player player)
    {
        player.transform.DOMove(transform.position, player.Speed).
        SetSpeedBased().
        SetEase(Ease.Linear);
        ServiceLocator.Instance.Get<SoundManager>().PlaySound(_playerLoseSFX);
        ServiceLocator.Instance.Get<GameManager>().PlayerDied();
    }
}
