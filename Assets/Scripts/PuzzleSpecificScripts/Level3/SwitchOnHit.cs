using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOnHit : MonoBehaviour, IHitable
{
    #region SetUp options
    [Header("Setup Options")]

    [Tooltip("Disappear the appearGameObjects will load on load")]
    [SerializeField]
    private bool disappearOnLoad = false;
    #endregion

    #region Game Objects
    [Header("Game Objects")]

    [Tooltip("The gameobjects that will be deactive when target is hit")]
    [SerializeField]
    private GameObject[] disapearGameObjects;

    [Tooltip("The gameobjects that will be activated when target is hit")]
    [SerializeField]
    private GameObject[] appearGameObjects;

    #endregion

    #region Unity Methods
    private void Start()
    {
        if (disappearOnLoad)
        {
            SetGameObjects(appearGameObjects, false);
        }
    }
    #endregion

    #region GameObject alter Methods
    /// <summary>
    /// Sets weather an array of gameobjects
    /// </summary>
    /// <param name="gameObjects"></param>
    ///     The array of game objects which active state is being changed
    /// <param name="isActive"></param>
    ///     Weather it is appeared or not
    private void SetGameObjects(GameObject[] gameObjects, bool isActive)
    {
        //Set the objects if there are objects to be set
        if (gameObjects.Length > 0)
        {
            foreach(var go in gameObjects)
            {
                go.SetActive(isActive);
            }
        }
    }
    #endregion

    public void IHit()
    {
        //Activate the appearing gameobjects
        SetGameObjects(appearGameObjects, true);

        //Deactivate the gameobjects if there is a gameobject there
        SetGameObjects(disapearGameObjects, false);

        //Disable this target
        this.gameObject.SetActive(false);
    }
}
