using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneSelect
{

    //Load the main menu directly
    public static void MainMenu()
    {
        //Loads main menu
        SceneManager.LoadScene("MainMenu");
    }
    
    //Load a level
    public static void LevelLoad(string levelName)
    {
        //Loads first level when it is ready
        SceneManager.LoadScene(levelName);
    }

    public static void Level2()
    {
        //Currently for testing Win Menu
        //Will load second level
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("LevelSwitchTest");
    }

    public static void RestartCurrentLevel()
    {
        //reloads the current active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void ExitGame()
    {
        //Quits to Desktop
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
