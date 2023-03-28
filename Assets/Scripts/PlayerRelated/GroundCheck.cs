using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to specifically check if the player si grounded
///     with additional settings just in case of Cyote time
/// </summary>
public class GroundCheck : MonoBehaviour
{
    #region Grounded Variables
    [Header("Grounded Varaibles")]

    [Tooltip("The grounded variable being read")]
    [SerializeField]
    private bool isGrounded = true;

    [Tooltip("Tests if the player is grounded")]
    [SerializeField]
    private bool isTouchingGround = true;
    #endregion

    #region Timer Variables
    [Header("Kyote Timer")]

    [Tooltip("The delay when the isGrounded is checked whenever a player runs off the edge in seconds.\n" +
        "[WARNING: Use small numbers, big ones could confuse jump]")]
    [SerializeField, Min(0f)]
    private float kyoteTime;

    [Tooltip("Keeps track of time of the kyote time")]
    [SerializeField, Min(0f), HideInInspector]
    private float kyoteTimer = 0f;
    #endregion

    #region Kyote Timer Methods
    /// <summary>
    /// Sets the timer to zero
    /// </summary>
    private void SetTimerToZero()
    {
        kyoteTimer = 0f;
    }

    /// <summary>
    /// Updates the timer with the change in time
    /// </summary>
    /// <param name="deltaTime"></param>
    private void UpdateTimer(float deltaTime)
    {
        if (kyoteTimer <= 0f)
            kyoteTimer = 0f;
        else
            kyoteTimer -= deltaTime;
    }

    /// <summary>
    /// Sets the time to the specified kyote time
    /// </summary>
    private void SetTimerToKyoteTime()
    {
        kyoteTimer = kyoteTime;
    }
    #endregion

    #region Access Methods
    /// <summary>
    /// Checks if grounded is true or not
    /// </summary>
    /// <returns></returns>
    public bool IsGrounded()
    {
        //Returns if grounded or not
        return isGrounded;
    }

    public bool GetIsTouchingGround()
    {
        return isTouchingGround;
    }
    #endregion

    #region Calculation Methods
    /// <summary>
    /// Checks if the player is touching the ground
    /// </summary>
    /// <returns></returns>
    private bool IsTouchingGround()
    {
        Vector3 center = transform.position;
        Vector3 halfExtents = this.gameObject.transform.lossyScale * (0.5f) + Vector3.down * 0.1f;
        Vector3 direction = Vector3.down;
        Quaternion rotation = transform.rotation;
        float distance = 1f;

        return Physics.BoxCast(center, halfExtents, direction, rotation, distance);
    }
    #endregion

    #region Unity Methods
    private void Update()
    {
        //Calculated if touching ground
        isTouchingGround = IsTouchingGround();

        //On First jump, if kyote timer is 
        if (isTouchingGround == false && kyoteTimer == 0f)
            SetTimerToKyoteTime();
        else if (isTouchingGround == false && kyoteTimer > 0f)
            UpdateTimer(Time.fixedDeltaTime);
        else if (isTouchingGround == true)
            SetTimerToZero();

        //The final determinant if is grounded is true. 
        //  if the player is touching the ground + kyote timer delay
        //  is still counting
        isGrounded = isTouchingGround || kyoteTimer > 0f;
    }
    #endregion
}
