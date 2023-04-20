using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantFallingPlatform : MonoBehaviour
{
    #region Data Variables
    [Header("Speed related")]

    [Tooltip("The speed at which the platform is falling" +
        " in meters per second")]
    [SerializeField, Min(0f)]
    private float fallSpeed = 1f;
    #endregion

    #region Unity Methods
    private void Update()
    {
        if (!PlayerStatic.HasConversation())
            this.transform.position += Vector3.down * fallSpeed * Time.deltaTime;
    }
    #endregion
}
