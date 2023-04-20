using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// Dictates the boss's movement
/// </summary>
public class BossMovement : MonoBehaviour
{
    #region Transform Reference
    [Header("Transform Reference")]

    [SerializeField]
    private Transform transformRef;
    #endregion

    #region This components
    [Header("Self components")]

    [Tooltip("The rigidbody allowing this boss to move")]
    [SerializeField]
    private Rigidbody rigidbody;
    #endregion

    #region Movement Variables
    [Header("Movement Variables")]

    [SerializeField, Min(0.01f)]
    private float speed = 1f;

    [Tooltip("Checks if the collision is within a distance of the position")]
    [SerializeField, Min(0.01f)]
    private float checkDistance = 0.25f;
    #endregion

    #region Offset Variables
    [Header("Offsets")]

    [Tooltip("The offset being targetted by the boss in meters")]
    [SerializeField]
    private float yOffset = 0f;
    #endregion

    #region Target Position
    [Header("The target position")]

    [Tooltip("The target's position combined with offset")]
    [SerializeField, HideInInspector]
    private Vector3 targetPosition = Vector3.zero;

    /// <summary>
    /// Updates the target position variable
    /// </summary>
    private void UpdateTargetPosition()
    {
        targetPosition = new Vector3(
            transformRef.position.x,
            transformRef.position.y + yOffset,
            transformRef.position.z);
    }
    #endregion

    #region Unity Methods
    private void Awake()
    {
        rigidbody = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //TODO: Get target's position
        UpdateTargetPosition();

        //TODO: Get the distance vector to the target position
        Vector3 distanceVector = targetPosition - this.transform.position;

        //TODO: Ge the distance
        float distance = distanceVector.magnitude;

        //TODO: Get the direction
        Vector3 direction = distanceVector.normalized;

        //TODO: Check if this is within the check boundary (if so, do not move)
        if (!(distance < checkDistance) && !PlayerStatic.HasConversation())
            //TODO: Move towards the transform reference
            rigidbody.velocity = direction * speed;
        else
            rigidbody.velocity = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(targetPosition, 1f);
    }
    #endregion
}
