using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to display dialogue without a time limit
/// </summary>
public class SequentialNPCDialogueDisplay : NPCDialogueDisplay
{
    #region Display Methods
    //!===================Methods====================!//
    /// <summary>
    /// Displays the current dialogue
    /// </summary>
    public void DisplayNextDialogue()
    {
        //Increment next dialogue
        IncrementDialogueIndex();


        //Conditionals to reset
        if (conversation == null)
        {
            ResetDisplay();
            Debug.LogError($"{this.gameObject} does not have a conversation");
            return;
        }

        if (dialogueIndex >= conversation.ConversationLength 
            || conversation.GetFormattedText(dialogueIndex) == null)
        {
            ResetDisplay();
            Debug.LogError($"{this.gameObject} does not have corrisponding dialogue" +
                $" to the dialogue index");
        }

        //Displays  the dialogue
        DisplayDialogue();
    }
    #endregion
}
