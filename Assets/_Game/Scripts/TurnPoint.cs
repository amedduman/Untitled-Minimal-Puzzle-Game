using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPoint : TileFeature
{
    [SerializeField] Sprite _turnPointSprite;

    public override void Reset()
    {
        base.Reset();

        if(_turnPointSprite != null)
            MyTile.TileSpriteRenderer.sprite = _turnPointSprite;
        
        
    }

    public Vector3 GetTurnDirection(Transform player)
    {
        // Vector3 turnDirOne = transform.GetChild(0).right;
        // Vector3 turnDirTwo = -transform.GetChild(0).up;

        // turnDirOne = transform.TransformDirection(turnDirOne);
        // turnDirTwo = transform.TransformDirection(turnDirTwo);

        // Debug.DrawRay(transform.GetChild(0).position, turnDirOne, Color.red, 1);
        // Debug.DrawRay(transform.GetChild(0).position, turnDirTwo, Color.green, 1);

        // float dotOne = Vector3.Dot(turnDirOne.normalized, player.up);
        // float dotTwo = Vector3.Dot(turnDirTwo.normalized, player.up);
        // Debug.Log(dotOne);
        // Debug.Log(dotTwo);

        // if(dotOne == 0)
        // {
        //     return transform.TransformDirection(turnDirOne);
        // }
        // else if( dotTwo == 0)
        // {
        //     return transform.TransformDirection(turnDirTwo);
        // }


        var playerUp = player.up;

        Vector3 tpUp = -transform.GetChild(0).transform.up;
        Vector3 tpRight = transform.GetChild(0).transform.right;

        if(playerUp == tpUp || playerUp == -tpUp)
        {
            return tpRight;
        }
        else if(playerUp == tpRight || playerUp == -tpRight)
        {
            return tpUp;
        }


        Debug.LogError("there is a problem with calculating the direction of turn point");
        Debug.Break();
        throw new System.NotImplementedException();
    }
}
