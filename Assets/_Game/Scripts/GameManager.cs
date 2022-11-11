using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public event Action OnPlayerDied;


    public void PlayerDied() 
    {
        OnPlayerDied?.Invoke();
    }
}
