using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == PlayerStatic.PlayerTag)
            ResetCurrentLevel();
    }

    /// <summary>
    /// Reset the current level
    /// </summary>
    public void ResetCurrentLevel()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }
}
