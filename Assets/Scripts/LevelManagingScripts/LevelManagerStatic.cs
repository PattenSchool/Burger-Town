using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelManagerStatic
{
    /// <summary>
    /// Loads a specific level by string name
    /// </summary>
    /// <param name="levelName"></param>
    public static void LoadSpecificLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    /// <summary>
    /// Increments a level
    /// </summary>
    public static void IncrementLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Dectrements a level
    /// </summary>
    public static void DecrementLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    /// <summary>
    /// Increments a level with a player as an incoming object
    ///     (meant for collisions and triggers specifically)
    /// </summary>
    /// <param name="incomingPlayer"></param>
    ///     The incoming player object
    public static void IncrementByPlayer(GameObject incomingPlayer)
    {
        if (incomingPlayer.tag == PlayerStatic.PlayerTag)
        {
            IncrementLevel();
        }
    }

    public static void ResetLevel()
    {
        //if (CheckPointManager.instance != null)
        //{
        //    CheckPointManager.instance.ClearList();
        //}


        if (CheckPointManager.instance != null)
        {
            CheckPointManager.instance.RestartLevelByManager();
            return;
        }

        Scene currentScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(currentScene.name);
    }
}
