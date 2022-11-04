using UnityEngine;
using AD.Utils.Math;

public class TurnPoint : Tile
{
    [SerializeField] Transform _firstPoint;
    [SerializeField] Transform _secondPoint;

    // change this logic. add two objects for 
    // public Vector3 GetTurnDirection(Transform player)
    // {
    //     var playerUp = player.up;

    //     Vector3 tpUp = -transform.transform.up;
    //     Vector3 tpRight = transform.transform.right;

    //     if(playerUp == tpUp || playerUp == -tpUp)
    //     {
    //         return tpRight;
    //     }
    //     else if(playerUp == tpRight || playerUp == -tpRight)
    //     {
    //         return tpUp;
    //     }

    //     Debug.LogError("there is a problem with calculating the direction of turn point");
    //     Debug.Break();
    //     throw new System.NotImplementedException();
    // } 

    public Vector3 GetTurnDirection(Transform player)
    {
        Vector3 playerDir = player.up;

        Vector3 firstDir = (_firstPoint.position - transform.position).normalized;
        Vector3 secondDir = (_secondPoint.position - transform.position).normalized;

        float firstDot = Vector3.Dot(firstDir, playerDir);
        float secondDot = Vector3.Dot(secondDir, playerDir);

        if(AD_MathUtils.CloseTo(0, firstDot, .5f))
        {
            return firstDir;
        }
        else if(AD_MathUtils.CloseTo(0, secondDot, .5f))
        {
            return secondDir;
        }

        Debug.LogError("there is a problem with calculating the direction of turn point");
        Debug.Break();
        throw new System.NotImplementedException();
    }

    // bool CloseTo(float val1, float val2, float margin)
    // {
    //     val1 = Mathf.Abs(val1);
    //     val2 = Mathf.Abs(val2);

    //     float diff = val1 - val2;

    //     diff = Mathf.Abs(diff);

    //     return diff <= margin;
    // }
}
