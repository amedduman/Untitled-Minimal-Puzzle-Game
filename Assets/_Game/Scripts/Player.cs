using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 4;
    Tweener _moveTweener;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryChangeDirection();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Move();
        }
    }

    void Move()
    {
        var currentTile = GetCurrentTilePlayerOn();
        var nextTile = GetNextTile(currentTile);
        if (nextTile != null)
        {
            _moveTweener = transform.DOMove(nextTile.transform.position, _speed).SetSpeedBased().OnComplete(Move).SetEase(Ease.Linear);
        }
        else
        {
            Vector3 rot = transform.rotation.eulerAngles;
            rot.z -= 180;
            transform.DORotate(rot, .3f).OnComplete(Move);
            
        }

    }

    void Turn(Vector3 dir)
    {
        Stop();
        transform.up = dir;
        Move();
    }

    void Stop()
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
        Physics2D.queriesStartInColliders = true;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward);
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
