using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rbCharacterControllerData : MonoBehaviour
{
    #region Data Variables
    [Header("Data variables")]

    [Tooltip("The rotation of the player in x and y in degrees")]
    [SerializeField]
    public Vector2 characterRotation = Vector2.zero;

    #endregion

    #region Component and game object variables
    [Header("Component Varibales")]

    [Tooltip("Used for camera postion and rotation information")]
    [SerializeField]
    public Transform cameraTransform = null;

    [Tooltip("Used for player position and rotation information")]
    [SerializeField]
    public Transform playerTransform = null;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        cameraTransform = Camera.main.GetComponent<Transform>();
        playerTransform = this.gameObject.GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        characterRotation = new 
            Vector2(cameraTransform.rotation.eulerAngles.x, 
            playerTransform.rotation.eulerAngles.y);
    }
    #endregion
}
