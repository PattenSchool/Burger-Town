using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

/// <summary>
/// Used to point things (like NPC dialogue) at the player
///     Note canvases may need additional rotation, set scale to -1 
///     on a canvas scale axis to see if it works.
/// </summary>
public class PointTowardsPlayer : MonoBehaviour
{
    #region Gameobejct
    //!===========Variables and Properties===========!//
    [Header("GameObejct reference")]

    [Tooltip("The gameobject being rotated towards the player")]
    [SerializeField, HideInInspector]
    private GameObject rotatingObejct;

    [ToolboxItem("The transform of the rotating object")]
    [SerializeField, HideInInspector]
    private Transform rotationTransform;
    #endregion

    #region Unity Methods
    private void Start()
    {
        rotatingObejct = this.gameObject;
        rotationTransform = rotatingObejct.transform;
    }

    private void Update()
    {
        //Set direction to the player
        rotationTransform.LookAt(PlayerStatic.MainCamera.transform.position);
    }
    #endregion
}
