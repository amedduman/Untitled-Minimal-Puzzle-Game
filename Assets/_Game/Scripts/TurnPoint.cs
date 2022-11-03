using UnityEngine;

public class TurnPoint : Tile
{
    // change this logic. add two objects for 
    public Vector3 GetTurnDirection(Transform player)
    {
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
