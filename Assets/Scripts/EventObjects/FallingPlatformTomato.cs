using System.Collections;
using UnityEngine;

/// <summary>
/// Controls the behaviors of the falling platform
/// </summary>
public class FallingPlatformTomato : MonoBehaviour
{
    #region Alterable related
    [Header("Falling related")]

    [Tooltip("The falling speed in meters")]
    [SerializeField, Min(0.01f)]
    private float fallingSpeed = 0.5f;

    [Tooltip("The max fall height")]
    [SerializeField, Min(0f)]
    private float fallDistance = 1f;

    [Tooltip("The delay before the fall starts in seconds")]
    [SerializeField, Min(0.1f)]
    private float delayTime = 0f;

    [Tooltip("The time while in fall in seconds")]
    [SerializeField, Min(0f)]
    private float fallingTime = 1f;
    #endregion

    #region Dynamic Variables
    [Header("Dynamic Variables")]

    [Tooltip("Hidden recorded speed")]
    [SerializeField, HideInInspector, Min(0f)]
    private float dynamicSpeed = 0f;
    #endregion

    #region Fixed Hidden Variables
    [Header("Fixed Variables")]

    [Tooltip("The original position")]
    [SerializeField, HideInInspector]
    private Vector3 originalPosition = Vector3.zero;

    [Tooltip("The final position the thing can be in")]
    [SerializeField, HideInInspector]
    private Vector3 finalPosition = Vector3.zero;
    #endregion

    #region Falling Methods
    private void StartFall()
    {
        dynamicSpeed = fallingSpeed;
    }

    private void StopFall()
    {
        dynamicSpeed = 0f;
    }
    #endregion

    #region Position Related
    private void UpdatePosition()
    {
        this.transform.position += new Vector3(0f, -dynamicSpeed * Time.deltaTime, 0f);
    }

    private void StopOnHeightDifference()
    {
        if (IsBelowFinalHeight())
        {
            this.transform.position = finalPosition;
        }
    }

    private bool IsBelowFinalHeight()
    {
        return transform.position.y <= finalPosition.y;
    }

    private void CalculateFinalPosition()
    {
        finalPosition = originalPosition - new Vector3(0f, fallDistance, 0f);
    }

    private void SetToFinalPosition()
    {
        transform.position = finalPosition;
    }

    private void StoreOriginalPosition()
    {
        originalPosition = this.transform.position;
    }

    private void SetToOriginalPosition()
    {
        this.transform.position = originalPosition; 
    }
    #endregion

    #region Direct Platform Control Methods
    /// <summary>
    /// Resets the platform's speed and position
    /// </summary>
    public void ResetPlatform()
    {
        //TODO: Stop fall
        StopFall();

        //TODO: Reset Position
        SetToOriginalPosition();
    }

    public void TriggerFall()
    {
        //TODO: Start the coroutine
        StartCoroutine(StartFallingSequence());
    }

    private IEnumerator StartFallingSequence()
    {
        //TODO: Start the delay
        yield return new WaitForSeconds(delayTime);

        //TODO: Start the fall
        StartFall();

        //TODO: Start the fall counter
        yield return new WaitForSeconds(fallingTime);

        //TODO: Reset the platform
        ResetPlatform();

        //TODO: Stop All Coroutines
        StopAllCoroutines();
    }
    #endregion

    #region Unity Methods
    private void Awake()
    {
        //TODO: Get position info
        StoreOriginalPosition();
        CalculateFinalPosition();

        //TODO: Reset fall just in case
        StopFall();
    }

    private void Update()
    {

        if (!IsBelowFinalHeight())
        {
            UpdatePosition();
        }
        else
        {
            SetToFinalPosition();
        }
    }

    private void OnDrawGizmos()
    {
        //TODO: Calculate the final position with or without editor being on
        #region Calculate projected position
        Vector3 projectedPosition;
        if (finalPosition == Vector3.zero)
        {
            projectedPosition = this.transform.position - new Vector3(0f, fallDistance, 0f); 
        }
        else
        {
            projectedPosition = finalPosition;
        }
        #endregion

        //TODO: Draw the wirecube at the end
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(projectedPosition, transform.lossyScale);

        //TODO: Draw line from current position to projected position
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, projectedPosition);
    }
    #endregion
}
