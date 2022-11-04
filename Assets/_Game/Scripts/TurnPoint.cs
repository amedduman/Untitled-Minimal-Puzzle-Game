using UnityEngine;
using AD.Utils.Math;

public class TurnPoint : Tile
{
    [SerializeField] Transform _firstPoint;
    [SerializeField] Transform _secondPoint;

    public Vector3 GetTurnDirection(Transform player)
    {
        Vector3 playerDir = player.up;

        Vector3 firstDir = (_firstPoint.position - transform.position).normalized;
        Vector3 secondDir = (_secondPoint.position - transform.position).normalized;

        float margin = .9f;
        if(AD_MathUtils.DoesVectorsPerpendicular(playerDir, firstDir, margin))
        {
            return firstDir;
        }
        else if(AD_MathUtils.DoesVectorsPerpendicular(playerDir, secondDir, margin))
        {
            return secondDir;
        }

        Debug.LogError("there is a problem with calculating the direction of turn point");
        Debug.Break();
        throw new System.NotImplementedException();
    }
}
