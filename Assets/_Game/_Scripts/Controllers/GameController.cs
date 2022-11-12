using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    public event Action OnPlayerWin;
    public event Action OnPlayerDied;


    public void PlayerDied() 
    {
        OnPlayerDied?.Invoke();
    }

    public void PlayerWin()
    {
        OnPlayerWin();
    }
}
