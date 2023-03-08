using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == PlayerStatic.PlayerTag)
        {
            LevelManagerStatic.ResetLevel();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == PlayerStatic.PlayerTag)
        {
            LevelManagerStatic.ResetLevel();
        }
    }

    /// <summary>
    /// Reset the current level
    /// </summary>
    public void ResetCurrentLevel()
    {
    }
}
