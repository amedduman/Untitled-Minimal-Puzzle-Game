using DG.Tweening;
using MoreMountains.Feedbacks;
using UnityEngine;

public class FinishTile : Tile
{
    [SerializeField] MMF_Player _startToEnterToTileFeedback;
    
    public override void React(Player player)
    {
        player.transform.DOMove(transform.position, player.Speed * 3).
        SetSpeedBased().
        SetEase(Ease.Linear).
        OnComplete(CallToWinGame);
        player.transform.DOScale(transform.localScale * .2f, .1f);
        _startToEnterToTileFeedback.PlayFeedbacks();
    }

    void CallToWinGame()
    {
        _playerEnteredFb.PlayFeedbacks();
        ServiceLocator.Get<GameController>().PlayerWin();
    }
}
