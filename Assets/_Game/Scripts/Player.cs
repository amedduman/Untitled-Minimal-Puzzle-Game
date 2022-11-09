using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using AD.Utils.Math;
using Sirenix.OdinInspector;

public class Player : MonoBehaviour
{
    public float Speed = 4;
    LayerMask _layerToRaycast;
    bool _firstTap = true;

    void Awake()
    {
        _layerToRaycast = LayerMask.GetMask("Tile");
    }

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

    // change this logic
    Tile GetNextTile(Tile currentTile)
    {
        float margin = .3f;

        if(AD_MathUtils.DoesVectorsLookAtSameDirection(Vector3.up, transform.up, margin))
        {
            return currentTile.upNeighbor;
        }
        else if(AD_MathUtils.DoesVectorsLookAtSameDirection(Vector3.down, transform.up, margin))
        {
            return currentTile.downNeighbor;
        }
        else if(AD_MathUtils.DoesVectorsLookAtSameDirection(Vector3.right, transform.up, margin))
        {
            return currentTile.rightNeighbor;
        }
        else if(AD_MathUtils.DoesVectorsLookAtSameDirection(Vector3.left, transform.up, margin))
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
