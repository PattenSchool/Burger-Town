using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelect : MonoBehaviour
{
  
     void Start()
    {

    }

    //Load the main menu directly
    public void MainMenu()
    {
        //Loads main menu
        SceneManager.LoadScene("MainMenu");
    }
    
    //Load a level
    public void LevelLoad(string levelName)
    {
        //Loads first level when it is ready
        SceneManager.LoadScene(levelName);
    }

    public void Level2()
    {
        //Currently for testing Win Menu
        //Will load second level
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("LevelSwitchTest");
    }

    public void RestartCurrentLevel()
    {
        //reloads the current active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        //Quits to Desktop
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
