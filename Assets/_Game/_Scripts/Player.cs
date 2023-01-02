using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using amed.utils.math;
using amed.utils.sound;
using MoreMountains.Feedbacks;

public class Player : MonoBehaviour
{
    public float Speed = 4;
    [SerializeField] Transform _body;
    [SerializeField] MMF_Player _playerDiedFb;
    [SerializeField] MMF_Player _playerReflectedFb;
    GameController _gameMng;
    LayerMask _layerToRaycast;
    bool _firstTap = true;

    void Awake()
    {
        _layerToRaycast = LayerMask.GetMask("Tile");
        _gameMng = ServiceLocator.Get<GameController>();
    }

    void OnEnable()
    {
        _gameMng.OnPlayerDied += HandleDeath;
    }

    void OnDisable()
    {
        _gameMng.OnPlayerDied -= HandleDeath;

        DOTween.Kill(transform);
    }

    void Update()
    {
        // if (Input.touchCount > 0)
        // {
        //     if (_firstTap)
        //     {
        //         Move();
        //         _firstTap = false;
        //     }
        //     else
        //     {
        //         TryChangeDirection();
        //     }
        // }

        // enable codes below
        // if (Input.touchCount >= 1)
        // {
        //     if (Input.touches[0].phase == TouchPhase.Began)
        //     {
        //         if (_firstTap)
        //         {
        //             Move();
        //             _firstTap = false;
        //         }
        //         else
        //         {
        //             TryChangeDirection();
        //         }
        //     }
        // }

        if (Input.GetMouseButtonDown(0))
        {
            if (_firstTap)
            {
                Move();
                _firstTap = false;
            }
            else
            {
                TryChangeDirection();
            }
        }
    }

    void HandleDeath()
    {
        _playerDiedFb.PlayFeedbacks();
    }

    public void Move()
    {
        var currentTile = GetCurrentTilePlayerOn();
        var nextTile = GetNextTile(currentTile);
        if (nextTile != null)
        {
            nextTile.React(this);
        }
        else
        {
            Reflect();
        }
    }

    public void Reflect()
    {
        _playerReflectedFb.PlayFeedbacks();
        Vector3 rot = transform.rotation.eulerAngles;
        rot.z -= 180;
        transform.DORotate(rot, .3f).OnComplete(Move);
    }

    void Turn(Vector3 dir)
    {
        Stop();
        transform.up = dir;
        Move();
    }

    public void Stop()
    {
        DOTween.Kill(transform);
    }

    void TryChangeDirection()
    {
        var tile = GetCurrentTilePlayerOn();

        if (tile.TryGetComponent(out TurnPoint turnPoint))
        {
            turnPoint.PlayerTapToTurn();
            var turnInfo = turnPoint.GetTurnInfo(transform);
            Turn(turnInfo.Dir);
        }
    }

    public Tile GetCurrentTilePlayerOn()
    {
        // other wise raycast is not going to detect tile
        Physics2D.queriesStartInColliders = true;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward, 1, _layerToRaycast);
        if (hit.collider != null)
        {
            var go = hit.collider.gameObject;
            var tile = go.GetComponentInParent<Tile>();
            return tile;
        }
        Debug.LogError("There is an error while getting Current Tile.");
        throw new System.NotImplementedException();
    }

    Tile GetNextTile(Tile currentTile)
    {
        float margin = .3f;

        if (adMath.DoesVectorsLookAtSameDirection(Vector3.up, transform.up, margin))
        {
            return currentTile.upNeighbor;
        }
        else if (adMath.DoesVectorsLookAtSameDirection(Vector3.down, transform.up, margin))
        {
            return currentTile.downNeighbor;
        }
        else if (adMath.DoesVectorsLookAtSameDirection(Vector3.right, transform.up, margin))
        {
            return currentTile.rightNeighbor;
        }
        else if (adMath.DoesVectorsLookAtSameDirection(Vector3.left, transform.up, margin))
        {
            return currentTile.leftNeighbor;
        }

        else
        {
            Debug.Log("There is a problem while getting Next tile");
            Debug.Break();
            return null;
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Set Player Tile")]
    void SetPlayerTile()
    {
        _layerToRaycast = LayerMask.GetMask("Tile");
        var tile = GetCurrentTilePlayerOn();

        transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, transform.position.z);
    }
#endif
}
