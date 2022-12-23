using System;
using System.Collections;
using System.Collections.Generic;
using amed.utils.serviceLoc;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlowController : MonoBehaviour
{
    [SerializeField] LevelDataContainer _lvlDataCnt;
    string _defaultLevelToLoadIfThereIsNoSavedLevelName;
    const string _levelIndexKey = "Level Index";

    void Start()
    {
        _defaultLevelToLoadIfThereIsNoSavedLevelName = _lvlDataCnt.GetFirstLevelName();
        LoadSavedLevel();
    }

    public void LoadSavedLevel()
    {
        var savedLvlName = GetSavedLevelName();

        UnloadPreviousLevel(savedLvlName);

        SceneManager.LoadSceneAsync(savedLvlName, LoadSceneMode.Additive);
    }

    public void LoadNextLevel()
    {
        var savedLvlName = GetSavedLevelName();

        UnloadPreviousLevel(savedLvlName);

        var nextLvlName = GetNextLevelName(savedLvlName);

        SceneManager.LoadSceneAsync(nextLvlName, LoadSceneMode.Additive);

        SetSavedLevelName(nextLvlName);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            LoadNextLevel();
        }
    }

    void UnloadPreviousLevel(string lvlName)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if(SceneManager.GetSceneAt(i).name == lvlName)
            {
                SceneManager.UnloadSceneAsync(lvlName);
            }
        }
    }

    string GetSavedLevelName()
    {
        return PlayerPrefs.GetString(_levelIndexKey, _defaultLevelToLoadIfThereIsNoSavedLevelName);
    }

    void SetSavedLevelName(string lvlName)
    {
        PlayerPrefs.SetString(_levelIndexKey, lvlName);
    }

    string GetNextLevelName(string savedLvlName)
    {
        for (int i = 0; i < _lvlDataCnt.Levels.Count; i++)
        {
            if(savedLvlName == _lvlDataCnt.Levels[i].SceneName)
            {
                if(i + 1 >= _lvlDataCnt.Levels.Count)
                {
                    return _lvlDataCnt.GetFirstLoopableLevel();;
                }
                else
                {
                    return _lvlDataCnt.Levels[i + 1].SceneName;
                }
            }
        }

        throw new System.NotImplementedException();
    }

}
