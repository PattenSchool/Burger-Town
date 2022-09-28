using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelect : MonoBehaviour
{
    public void MainMenu()
    {
        //Loads main menu
        SceneManager.LoadScene("MainMenu");
    }
    
    public void Level1()
    {
        //Loads first level when it is ready
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {
        //Quits to Desktop
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
