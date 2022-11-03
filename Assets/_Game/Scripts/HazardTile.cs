using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HazardTile : Tile
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.TryGetComponent(out Player player))
        {
            player.Stop();
            player.transform.DOMove(transform.position, 2).SetSpeedBased().SetEase(Ease.Linear);
        }
    }
}
