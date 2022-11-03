using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Player : MonoBehaviour
{
    public float Speed = 4;
    [SerializeField] LayerMask _layerToRaycast;
    Tweener _moveTweener;
    bool _firstTap = true;

    void Update()
    {
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

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Stop();
        }
    }

    void Move()
    {
        var currentTile = GetCurrentTilePlayerOn();
        var nextTile = GetNextTile(currentTile);
        if (nextTile != null)
        {
            _moveTweener = nextTile.React(this, Move);
            // _moveTweener = transform.DOMove(nextTile.transform.position, _speed).SetSpeedBased().OnComplete(Move).SetEase(Ease.Linear);
        }
        else
        {
            Reflect();
        }
    }

    public void Reflect()
    {
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
        _moveTweener.Kill();
    }

    void TryChangeDirection()
    {
        var tile = GetCurrentTilePlayerOn();

        if (tile.TryGetComponent(out TurnPoint turnPoint))
        {
            Vector3 dir = turnPoint.GetTurnDirection(transform);
            Turn(dir);
        }
    }

    Tile GetCurrentTilePlayerOn()
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
        var worldUp = Vector3.up;
        var worldDown = Vector3.down;
        var worldRight = Vector3.right;
        var worldLeft = Vector3.left;

        var playerUp = transform.up;

        if (playerUp == worldUp)
        {
            return currentTile.upNeighbor;
        }
        else if (playerUp == worldDown)
        {
            return currentTile.downNeighbor;
        }
        else if (playerUp == worldRight)
        {
            return currentTile.rightNeighbor;
        }
        else if (playerUp == worldLeft)
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

    [PropertySpace]
    [Button]
    void SetPlayerTile()
    {
        var tile = GetCurrentTilePlayerOn();

        transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, transform.position.z);
    }
}
