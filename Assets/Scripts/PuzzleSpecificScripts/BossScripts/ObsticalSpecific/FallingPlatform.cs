using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    #region Comopnent Related
    [Header("Components")]

    [Tooltip("The rigidbody controlling this object's velocity")]
    [SerializeField, HideInInspector]
    private Rigidbody platformRigidBody;
    #endregion

    #region Data Variabes
    [Header("Data related")]

    [Tooltip("The speed at which the falling object is falling at")]
    [SerializeField, Min(0f)]
    private float fallingSpeed;

    [Tooltip("The ammount of time spent falling before resetting in seconds")]
    [SerializeField, Min(0f)]
    private float fallingTime = 0.1f;

    [Tooltip("The height difference this platform is allowed to fall in meters")]
    [SerializeField, Min(0.1f)]
    private float maxFallDistance = 0.1f;

    [Tooltip("The delay when the fall is called to start")]
    [SerializeField, Min(0f)]
    private float fallDelay = 0f;

    [Tooltip("The original position of the platform")]
    [SerializeField, HideInInspector]
    private Vector3 originalPosition = Vector3.zero;
    #endregion

    #region Triggered Methods
    public void TriggerFall()
    {
        StartFalling();

        //TODO: Start the coroutine to start the fall
        StartCoroutine(SetFallWithTimer());

        
    }

    public IEnumerator SetFallWithTimer()
    {
        //TODO: Delay the fall
        yield return new WaitForSeconds(fallDelay);

        //TODO: Start the fall
        StartFalling();

        //TODO: Wait until stop falling and reset the block
        yield return new WaitForSeconds(fallingTime);
        ResetPosition();
    }

    /// <summary>
    /// Checks if the thing is at fall distance
    /// </summary>
    public bool IsAtFallDistance()
    {
        float currentHeight = this.transform.position.y;

        float finalHeight = CalculateFinalPosition().y;

        return currentHeight <= finalHeight;
    }

    [ExecuteInEditMode]
    public Vector3 CalculateFinalPosition()
    {
        Vector3 startingPosition;

        if (originalPosition == Vector3.zero)
        {
            startingPosition = transform.position;
        }
        else
        {
            startingPosition = originalPosition;
        }

        return startingPosition - new Vector3(0f, maxFallDistance, 0f);
    }

    public void StartFalling()
    {
        
        //TODO: Set valocity
        platformRigidBody.velocity = Vector3.down * (fallingSpeed);

        print(platformRigidBody.velocity);
    }

    public void StopFalling()
    {
        //TODO: Freeze the velocity
        platformRigidBody.velocity = Vector3.zero;
    }

    /// <summary>
    /// Sets the original position in storage
    /// </summary>
    public void SetOriginalPosition()
    {
        originalPosition = transform.position;
    }

    public void ResetPosition()
    {
        //TODO: Stop the fall
        StopFalling();

        //TODO: Reset the object
        this.transform.position = originalPosition;
    }
    #endregion

    #region Unity Methods
    private void Awake()
    {
        SetOriginalPosition();
        platformRigidBody = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (IsAtFallDistance())
            StopFalling();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(CalculateFinalPosition(), this.transform.localScale);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != PlayerStatic.PlayerTag)
        {
            StopFalling();
        }
    }
    #endregion
}
