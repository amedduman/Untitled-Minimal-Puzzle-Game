using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 4;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Move();
        }
    }

    void Move() 
    {
        var currentTile = GetCurrentTilePlayerOn();
        var nextTile = GetNextTile(currentTile);
        if(nextTile != null)
        {
            transform.DOMove(nextTile.transform.position, _speed).SetSpeedBased().OnComplete(Move).SetEase(Ease.Linear);
        }

    }

    Tile GetCurrentTilePlayerOn()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward);

        if(hit.collider != null)
        {
            Debug.Log(hit.collider.transform.parent.name);
            var go = hit.collider.gameObject;
            var tile = go.GetComponentInParent<Tile>();
            return tile;
        }
        Debug.LogError("There is an error while getting Current Tile.");
        Debug.Break();
        return null;
    }

    Tile GetNextTile(Tile currentTile)
    {
        var worldUp = Vector3.up;
        var worldDown = Vector3.down;
        var  worldRight = Vector3.right;
        var worldLeft = Vector3.left;

        var playerUp = transform.up;

        if(playerUp == worldUp)
        {
            return currentTile.upNeighbor;
        }
        else if(playerUp == worldDown)
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
}
