using DG.Tweening;

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
        ServiceLocator.Get<GameController>().PlayerWin();
    }
}
