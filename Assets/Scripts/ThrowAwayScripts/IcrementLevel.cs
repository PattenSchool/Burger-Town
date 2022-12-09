using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IcrementLevel : MonoBehaviour, IObjectEvent
{
    [SerializeField]
    private SaveLoadManager _saveLoadManager;

    #region Unity Methods
    public void IOnEventTriggered()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void OnCollisionEnter(Collision collision)
    {
        IncremenetLevel(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        IncremenetLevel(other);
    }
    #endregion


    #region Other Methods
    /// <summary>
    /// Increments the level from the build index
    /// </summary>
    /// <param name="incomingCollider"></param>
    private void IncremenetLevel(Collider incomingCollider)
    {
        if (incomingCollider.gameObject.tag == PlayerStatic.PlayerTag)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    /// <summary>
    /// Increases the build level
    /// </summary>
    /// <param name="incomingCollision"></param>
    ///     The incoming collision
    private void IncremenetLevel(Collision incomingCollision)
    {
        IncremenetLevel(incomingCollision.collider);
    }
    #endregion
}
