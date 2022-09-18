using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GetCurrentTilePlayerOn();
        }
    }

    void GetCurrentTilePlayerOn()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward);

        if(hit.collider != null)
        {
            Debug.Log(hit.collider.transform.parent.name);
        }
    }
}
