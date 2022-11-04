using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AD.Utils.Math;

public class TestDirMatch : MonoBehaviour
{
    [SerializeField] float margin = .5f;

    void OnDrawGizmos()
    {
        // if(AD_MathUtils.DoesVectorsLookAtSameDirection(Vector3.right, transform.up, margin))
        // {
        //     Gizmos.color = Color.green;
        // }
        // else
        // {
        //     Gizmos.color = Color.red;
        // }

        if(AD_MathUtils.DoesVectorsPerpendicular(Vector3.up, transform.up, margin))
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }

        Gizmos.DrawSphere(transform.position, .1f);
    }
}
