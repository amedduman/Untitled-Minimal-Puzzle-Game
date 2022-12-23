using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using amed.utils.serviceLoc;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public event Action OnPlayerWin;
    public event Action OnPlayerDied;

    private SceneFlowController _sceneFlowController;

    private void Awake()
    {
        _sceneFlowController = ServiceLocator.Instance.Get<SceneFlowController>();
    }

    public void PlayerDied() 
    {
        OnPlayerDied?.Invoke();
        DOVirtual.DelayedCall(1.5f, () => _sceneFlowController.LoadSavedLevel());
    }

    public void PlayerWin()
    {
        OnPlayerWin?.Invoke();
        DOVirtual.DelayedCall(1, () => _sceneFlowController.LoadNextLevel());
    }
}
