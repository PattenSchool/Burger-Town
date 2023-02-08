using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used to toggle the reticle when conversations come up
/// </summary>
public class DialogueReticleToggle : MonoBehaviour
{
    #region Reticle Elemtents
    //!===========Variables and Properties===========!//
    [Header("Reticle Elements")]

    [Tooltip("The reticle Image")]
    [SerializeField]
    private Image reticle;

    [Tooltip("The cooldown on display")]
    [SerializeField]
    private Image cooldownDisplay;
    #endregion

    #region disapear toggle
    //!===========Variables and Properties===========!//
    [Header("Toggles")]

    [Tooltip("Weather the reticle is off durring dialogue exchanges")]
    [SerializeField]
    private bool displayWhileConvo = true;
    #endregion

    #region Toggle Methods
    //!===================Methods====================!//
    /// <summary>
    /// Toggles the reticle elements if displayed or not
    /// </summary>
    /// <param name="toggleOverride"></param>  
    ///     If true, then reticle elements are displayed
    public void ToggleReticleElements(bool toggleOverride)
    {
        reticle.enabled = toggleOverride;
        cooldownDisplay.enabled = toggleOverride;
    }

    /// <summary>
    /// Switch reticle elements enableability.
    ///     if enabled, then disable.
    ///     else, then enable.
    /// </summary>
    public void ToggleReticleElements()
    {
        ToggleReticleElements(!reticle.enabled);
    }

    /// <summary>
    /// Overrides the reticle toggle only if allowed by the display bool
    /// </summary>
    /// <param name="toggleOverride"></param>
    ///     Set reticle elements manually if text allows it from the inspector
    public void ConditionalToggleReticleUI(bool toggleOverride)
    {
        if (displayWhileConvo)
            ToggleReticleElements(toggleOverride);
    }

    /// <summary>
    /// Toggles reticle UI if displayed
    /// </summary>
    public void ConditionalToggleReticleUI()
    {
        if (displayWhileConvo)
            ToggleReticleElements();
    }
    #endregion

    #region Unity Methods
    //!===================Methods====================!//
    private void Update()
    {
        //Disable reticle if player has conversation
        ConditionalToggleReticleUI(PlayerStatic.Conversation == null);
    }
    #endregion
}
