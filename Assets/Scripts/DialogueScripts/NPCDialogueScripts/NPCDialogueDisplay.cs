using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Displays the dialogue of NPCs
/// </summary>
public class NPCDialogueDisplay : MonoBehaviour
{
    #region Display Variables
    //!===========Variables and Properties===========!//
    [Header("UI Elements")]

    [Tooltip("The Text display carrying the text")]
    [SerializeField]
    private TMP_Text textDisplay;
    #endregion

    #region Conversation Variables
    //!===========Variables and Properties===========!//
    [Header("Conversation Related")]

    [Tooltip("Possible dialogue options")]
    [SerializeField]
    private Conversation_SO npcConversation;

    [Tooltip("Start randomizing at this index onwards" +
        "if randomizing is greater than possible dialogues, " +
        "then randomizing doesn't happen.")]
    [SerializeField]
    private int startRandomizingAt = 9999;
    #endregion

    #region Conversation Methods
    //!===================Methods====================!//
    /// <summary>
    /// Overrides the conversation 
    /// </summary>
    /// <param name="newConversation"></param>
    ///     The conversation that is replacing the old or null conversation
    public void OverrideConversation(Conversation_SO newConversation)
    {
        npcConversation = newConversation;
    }

    /// <summary>
    /// Returns true if a conversation is in place
    /// </summary>
    /// <returns></returns>
    ///     Returns true if conversation is held
    public bool HasConversation()
    {
        return npcConversation != null;
    }

    public void DeleteConversation()
    {
        npcConversation = null;
    }
    #endregion
}
