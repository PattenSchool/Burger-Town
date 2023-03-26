using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Matches the attached gameobject's position to the reference in here
/// </summary>
public class MatchGameObjectTransform : MonoBehaviour
{
    #region Followable GameObject
    [Header("Transform Reference")]

    [Tooltip("What gameobject the transform is following")]
    [SerializeField]
    private Transform followableTransform;
    #endregion

    #region Position Options
    [Header("Transform Follow Options")]
    [Header("Position options")]

    [SerializeField]
    private bool freezeXPosition = false;

    [SerializeField]
    private bool freezeYPosition = false;

    [SerializeField]
    private bool freezeZPosition = false;

    /// <summary>
    /// Updates the position of this gameobject with the followable gameobject
    /// </summary>
    private void UpdatePosition()
    {
        //TODO: Get the original axis floats
        float x = this.transform.position.x;
        float y = this.transform.position.y;
        float z = this.transform.position.z;

        //TODO: Get new position of the target object
        float newX = followableTransform.position.x;
        float newY = followableTransform.position.y;
        float newZ = followableTransform.position.z;

        //TODO: Update the axis floats if allowed
        SetAxisPosition(ref x, newX, freezeXPosition);
        SetAxisPosition(ref y, newY, freezeYPosition);
        SetAxisPosition(ref z, newZ, freezeZPosition);

        //TODO: Update with new position
        this.transform.position = new Vector3(x, y, z);
    }

    /// <summary>
    /// Used to update this objects position if the freeze position option isn't true
    /// </summary>
    /// <param name="originalPosition"></param>
    /// <param name="newPosition"></param>
    /// <param name="isFrozen"></param>
    private void SetAxisPosition(ref float originalPosition, float newPosition, bool isFrozen)
    {
        if (!isFrozen)
            originalPosition = newPosition;
    }
    #endregion

    #region Rotation Options
    [Header("Rotation options")]

    [Tooltip("The state of rotation goal")]
    [SerializeField]
    private LookState lookState = LookState.lookAtObject;
    private enum LookState
    {
        lookAtObject,
        matchObjectRotation
    }

    [Tooltip("Freeze rotation axis")]
    [SerializeField]
    private bool freezeXRotation = false;


    [Tooltip("Freeze rotation axis")]
    [SerializeField]
    private bool freezeYRotation = false;

    [Tooltip("Freeze rotation axis")]
    [SerializeField]
    private bool freezeZRotation = false;

    private void UpdateRotation()
    {
        //TODO: Get the rotation 
        Vector3 oldRotation = this.transform.rotation.eulerAngles;
        float oldX = oldRotation.x;
        float oldY = oldRotation.y;
        float oldZ = oldRotation.z;

        //Initialize new rotations
        float newX = oldX;
        float newY = oldY;
        float newZ = oldZ;

        //TODO: Calculate the new rotation
        if (lookState == LookState.lookAtObject)
        {
            //TODO: Set rotation of the transform of this object to the player
            this.transform.LookAt(followableTransform);

            //TODO: Get the new floats
            Vector3 newRotation = this.transform.rotation.eulerAngles;

            //TODO: Set the rotations if frozen
            SetAxisRotation(ref newX, newRotation.x, freezeXRotation);
            SetAxisRotation(ref newY, newRotation.y, freezeYRotation);
            SetAxisRotation(ref newZ, newRotation.z, freezeZRotation);

            //TODO: Reset rotation
            this.transform.rotation = Quaternion.Euler(oldX, oldY, oldZ);
        }
        else if (lookState == LookState.matchObjectRotation)
        {
            //TODO: Get the new transform
            Vector3 newRotation = followableTransform.rotation.eulerAngles;

            //TODO: Set the rotations if frozen
            SetAxisRotation(ref newX, newRotation.x, freezeXRotation);
            SetAxisRotation(ref newY, newRotation.y, freezeYRotation);
            SetAxisRotation(ref newZ, newRotation.z, freezeZRotation);
        }

        //TODO: Set the rotation to this object's transform
        this.transform.rotation = Quaternion.Euler(newX, newY, newZ);
    }

    /// <summary>
    /// Changeas the rotation with frozen is taken into account
    /// </summary>
    /// <param name="changeableRotation"></param>
    /// <param name="newRotation"></param>
    /// <param name="isFrozen"></param>
    private void SetAxisRotation(ref float changeableRotation, float newRotation, bool isFrozen)
    {
        if (!isFrozen)
            changeableRotation = newRotation;
    }
    #endregion

    #region Unity Methods
    private void Update()
    {
        UpdatePosition();
        UpdateRotation();
    }
    #endregion
}
