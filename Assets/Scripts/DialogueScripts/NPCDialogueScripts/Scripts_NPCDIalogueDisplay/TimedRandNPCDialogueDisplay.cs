using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Displays a random dialogue in a Conversation_SO
/// </summary>
public class TimedRandNPCDialogueDisplay : RandomizedNPCDialogueDisplay
{
    #region Timer Related
    //!===========Variables and Properties===========!//
    [Header("Timer Elements")]

    [Tooltip("How much time each dialogue is shown")]
    [SerializeField, Min(0.1f)]
    private float dialogueShownSeconds = 1f;
    #endregion

    #region max dialogue displayed
    //!===========Variables and Properties===========!//
    [Header("Max Dialogue Chain")]

    [Tooltip("How many dialogues are displayed at a time")]
    [SerializeField, Min(1)]
    private int maxDialogueDisplayed = 1;

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
        for(int i = 0; i < maxDialogueDisplayed; i++)
        {
            //Display the dialogue
            DisplayDialogue();

            //Wait for the dialogue
            yield return new WaitForSeconds(dialogueShownSeconds);
        }

        //Reset the text
        ResetDialogueIndex();
        ResetDisplay();
    }
    #endregion
}
