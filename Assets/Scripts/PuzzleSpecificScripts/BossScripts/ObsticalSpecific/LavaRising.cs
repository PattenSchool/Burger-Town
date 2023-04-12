using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaRising : MonoBehaviour
{
    #region Components
    [Header("Components")]

    [Tooltip("The rigidbody controlling the vertical movement" +
        " of the lava")]
    [SerializeField, HideInInspector]
    private Rigidbody lavaRigidBody = null;
    #endregion

    #region Movement Variables
    [Header("Movement")]

    [Tooltip("The speed of the lava")]
    [SerializeField, Min(0f)]
    private float lavaSpeed = 0f;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        lavaRigidBody = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        lavaRigidBody.position += Vector3.up * lavaSpeed * Time.deltaTime;
    }
    #endregion
}
