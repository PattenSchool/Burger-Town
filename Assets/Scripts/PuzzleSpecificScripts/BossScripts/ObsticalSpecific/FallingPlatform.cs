using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class FallingPlatform : MonoBehaviour
{
    #region Components
    [Header("Components")]

    [Tooltip("The rigidbody that is used to control velocity")]
    [SerializeField]
    private Rigidbody platformRigidBody;
    #endregion

    #region Height related variables
    [Header("Height Related")]

    [Tooltip("The speed that is used when going down")]
    [SerializeField]
    private float fallingSpeed;

    [Tooltip("The time until reset after delay")]
    [SerializeField, Min(0f)]
    private float fallDelay = 0f;

    [Tooltip("The time in seconds to reset after delay")]
    [SerializeField, Min(0.1f)]
    private float fallTimer = 0.1f;

    [Tooltip("The max height the platform can be dropped in meters")]
    [SerializeField]
    private float fallHeight = 0f;
    #endregion

    #region Storage related
    [Header("Storage Related")]

    [SerializeField]
    private Vector3 originalPosition;

    private void SetOriginalPosition()
    {
        originalPosition = transform.position;
    }
    #endregion

    #region Falling Methods
    //TODO: Start the fall
    private void StartFall()
    {
        ////platformRigidBody.velocity = Vector3.down * fallingSpeed;
        //platformRigidBody.constraints = RigidbodyConstraints.FreezeAll | ~RigidbodyConstraints.FreezePositionY;
    }

    //TODO: Freezes the platform
    private void StopFall()
    {
        ////platformRigidBody.velocity = Vector3.zero;
        //platformRigidBody.constraints = RigidbodyConstraints.FreezeAll;
    }

    //TODO: Start fall from the outside
    public void TriggerFall()
    {
        StartFall();
        StartCoroutine(StartFallWithTimers());
    }

    public IEnumerator StartFallWithTimers()
    {
        //if (this.transform.position != originalPosition)
        //{
        //    yield break;
        //}

        ////TODO: Reset any forces on the platform before 

        ////TODO: Start the delay
        //yield return new WaitForSeconds(fallDelay);

        ////TODO: Start the fall
        //StartFall();

        //TODO: Start the timer until reset
        yield return new WaitForSeconds(fallTimer);
        ResetPlatform();

    }

    #endregion

    #region Unity methods
    private void Awake()
    {
        platformRigidBody = GetComponent<Rigidbody>();
        SetOriginalPosition();
    }


    #endregion

    #region Misc Methods
    private void ResetPlatform()
    {
        //StopFall();
        this.transform.position = originalPosition;
    }
    #endregion
}
