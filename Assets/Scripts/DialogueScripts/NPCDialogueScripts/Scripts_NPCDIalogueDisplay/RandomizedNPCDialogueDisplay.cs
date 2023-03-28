using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Displays a random dialogue from a Conversation_SO.
///     Any specifics on resetting text and changing dialogue.
/// </summary>
public class RandomizedNPCDialogueDisplay : NPCDialogueDisplay
{
    #region Display methods
    //!===================Methods====================!//
    /// <summary>
    /// Used to display a dialogue with a random number between 0 
    ///     and conversation length - 1
    /// </summary>
    public override void DisplayDialogue()
    {
        //Stop if conversation doesn't exist
        if (conversation == null)
        {
            Debug.LogError($"Conversation is not in {this.gameObject}");
            return;
        }

        //Get the dialogue
        dialogueIndex = Random.Range(0, conversation.ConversationLength);

        //display the dialogue at random interval
        textDisplay.text = conversation.GetFormattedText(dialogueIndex);
    }
    #endregion
}
