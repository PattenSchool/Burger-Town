using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Any change that is wanted in the world
///     while dialogue is set up is managed here
/// </summary>
public class DialogueWorldManager : MonoBehaviour
{
    #region Effect Toggles
    //!===========Variables and Properties===========!//
    [Header("Effect Toggles")]

    [Tooltip("If the world pauses durring dialogue exchanges")]
    [SerializeField]
    private bool pauseWorldOnExchange = true;
    #endregion
}
