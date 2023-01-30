using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationMatch : MonoBehaviour
{
    #region GameObject Variables
    [Header("Orientation Game Object")]

    [Tooltip("The game object being rotated")]
    [SerializeField]
    private Transform orientationReciever;

    [Tooltip("THe game object which orientation is being matched")]
    [SerializeField]
    private Transform orientationMatcher;
    #endregion

    #region Freeze Axis
    [Header("Axis Freeze")]

    [Tooltip("Freeze x axis rotation")]
    [SerializeField]
    private bool isXFrozen = false;

    [Tooltip("Freeze y axis rotation")]
    [SerializeField]
    private bool isYFrozen = false;

    [Tooltip("Freeze z axis rotation")]
    [SerializeField]
    private bool isZFrozen = false;
    #endregion

    private void Update()
    {

        orientationReciever.eulerAngles = new Vector3(
            isXFrozen ? orientationReciever.eulerAngles.x : orientationMatcher.eulerAngles.x,
            isYFrozen ? orientationReciever.eulerAngles.y : orientationMatcher.eulerAngles.y,
            isZFrozen ? orientationReciever.eulerAngles.z : orientationMatcher.eulerAngles.z);
    }

    /// <summary>
    /// Gets a frozen angle or not
    /// </summary>
    /// <param name="isFrozen"></param>
    ///     Checks if the angle is frozen
    /// <param name="recieverAngle"></param>
    ///     If frozen, don't change the angle
    /// <param name="matcherAngle"></param>
    ///     If not frozen, match angle
    /// <returns></returns>
    ///     The angle
    private float GetRotation(bool isFrozen, float recieverAngle, float matcherAngle)
    {
        return isFrozen ? recieverAngle : matcherAngle;
    }
}
