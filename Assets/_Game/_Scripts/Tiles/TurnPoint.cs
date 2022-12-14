using UnityEngine;
using amed.utils.math;
using amed.utils.sound;
using MoreMountains.Feedbacks;

public struct TurnInfo
{
    public Vector3 Dir { get; private set; }
    public SpriteRenderer Sprite { get; private set; }

    public TurnInfo(Vector3 dir, SpriteRenderer sprite)
    {
        Dir = dir;
        Sprite = sprite;
    }
}

public class TurnPoint : Tile
{
    [SerializeField] MMF_Player _playerTurnedFb;
    [SerializeField] Transform _pointA;
    [SerializeField] Transform _pointB;
    [SerializeField] SpriteRenderer _arrowImageA;
    [SerializeField] SpriteRenderer _arrowImageB;

    Player _player;
    bool _playerEntered = false;

    void Start()
    {
        _player = ServiceLocator.Get<Player>();
    }

    public override void React(Player player)
    {
        Move(player);
    }

    void Update()
    {
        if (this == _player.GetCurrentTilePlayerOn())
        {
            if (_playerEntered == false)
            {
                _playerEnteredFb.PlayFeedbacks();
                var turnInfo = GetTurnInfo(_player.transform);
                turnInfo.Sprite.color = Color.green;
                _playerEntered = true;
            }

        }
        else
        {
            _playerEntered = false;
            SetArrowColorsToNormal();
        }
    }

    public void PlayerTapToTurn()
    {
        _playerTurnedFb.PlayFeedbacks();
        SetArrowColorsToNormal();
    }

    public void SetArrowColorsToNormal()
    {
        _arrowImageA.color = Color.white;
        _arrowImageB.color = Color.white;
    }

    public TurnInfo GetTurnInfo(Transform player)
    {
        Vector3 playerDir = player.up;

        Vector3 directionA = (_pointA.position - transform.position).normalized;
        Vector3 directionB = (_pointB.position - transform.position).normalized;

        float margin = .9f;
        if (adMath.DoesVectorsPerpendicular(playerDir, directionA, margin))
        {
            return new TurnInfo(directionA, _arrowImageA);
        }
        else if (adMath.DoesVectorsPerpendicular(playerDir, directionB, margin))
        {
            return new TurnInfo(directionB, _arrowImageB);
        }

        Debug.LogError("there is a problem with calculating the direction of turn point");
        Debug.Break();
        throw new System.NotImplementedException();
    }
}
