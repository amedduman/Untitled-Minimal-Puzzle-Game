using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AD.Utils.Math;

public class TestDirMatch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [SerializeField] float margin = .5f;

    void OnDrawGizmos()
    {
        if(AD_MathUtils.DoesVectorsLookAtSameDirection(Vector3.right, transform.up, margin))
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
