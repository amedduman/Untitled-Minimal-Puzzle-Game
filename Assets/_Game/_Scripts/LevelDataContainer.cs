using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "SceneDataContainer", menuName = "Game/LevelDataContainer")]
public class LevelDataContainer : ScriptableObject
{
    public List<LevelData> Levels = new List<LevelData>();

    public string GetFirstLevelName()
    {
        return Levels[0].SceneName;
    }

    public string GetFirstLoopableLevel()
    {
        for (int i = 0; i < Levels.Count; i++)
        {
            if(Levels[i].IsLoopable)
            {
                return Levels[i].SceneName;
            }
        }
        Debug.LogError("There are no loopable level in level data container. I don't know what to do when all levels done.");
        throw new System.NotImplementedException();
    }
}

[System.Serializable]
public class LevelData 
{
    public bool IsLoopable;
    public string SceneName;
}