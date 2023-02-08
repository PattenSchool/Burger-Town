using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Goes through all of a conversation in a timly fassion
/// </summary>
public class TimedSequentialNPCDisplay : SequentialNPCDialogueDisplay
{
    #region Timer Related
    //!===========Variables and Properties===========!//
    [Header("Timer Elements")]

    [Tooltip("How much time each dialogue is shown")]
    [SerializeField, Min(0.1f)]
    private float dialogueShownSeconds = 1f;
    #endregion

    #region Dialogue Display 
    //!===================Methods====================!//
    /// <summary>
    /// Displays the dialogue that was timed per dialogue
    /// </summary>
    public IEnumerator DisplayTimedDialogue()
    {
        //Check if the dialoguye is set
        if (conversation == null)
        {
            Debug.LogError($"{this.gameObject} does not have a conversation stored");
            yield return null;
        }

        //Display all things
        for (int i = 0; i < conversation.ConversationLength; i++)
        {
            //Display the dialogue
            DisplayDialogue();

            //Increment the dialogue index
            IncrementDialogueIndex();

            //Wait for the dialogue
            yield return new WaitForSeconds(dialogueShownSeconds);
        }

        //Reset the text
        ResetDialogueIndex();
        ResetDisplay();
    }
    #endregion
}
