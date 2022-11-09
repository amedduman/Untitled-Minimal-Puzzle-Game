using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DefaultExecutionOrder(-10000)]
public class AutoRegister : MonoBehaviour
{
    [SerializeField] Component _cmp;

    void Awake()
    {
        Services.ServiceLocator.Instance.Register(_cmp);
    }
}
