using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCubeScript : MonoBehaviour
{
    #region Speed related
    [Header("Speed modifier")]

    [Tooltip("The rate at which speed is modified")]
    [SerializeField]
    private float speedModifier = 1f;

    private void UpdateSpeed(Rigidbody rigidbody)
    {
          
    }
    #endregion

    #region RigidBody reference
    [Header("Rigidbody reference")]

    [Tooltip("The rigidbody being manipulated, if rigidbody is null, nothing gets manipulated")]
    [SerializeField]
    private Rigidbody rigidBodyReference = null;
    #endregion

    #region Unity Methods
    private void OnCollisionEnter(Collision collision)
    {
        //TODO: Check if player
        if (collision.gameObject.tag == PlayerStatic.PlayerTag)
            rigidBodyReference = collision.rigidbody;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == PlayerStatic.PlayerTag)
            rigidBodyReference = null;
    }

    private void FixedUpdate()
    {
        if (rigidBodyReference != null)
        {
            UpdateSpeed(rigidBodyReference);
        }
    }
    #endregion
}
