using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        //Set up display
        SetChildrenVisibility(true);

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

        textDisplay.text = "";

        //Reset the text
        ResetDialogueIndex();
        ResetDisplay();
        SetChildrenVisibility(false);
    }

    public override void PlayConversation()
    {
        StopCoroutine(DisplayTimedDialogue());
        StartCoroutine(DisplayTimedDialogue());
    }
    #endregion

    #region Unity Methods
    private void Awake()
    {
        SetChildrenVisibility(false);
    }
    #endregion

    #region Children visibility
    /// <summary>
    /// Set the child elements of visibility
    /// </summary>
    /// <param name="isVisible"></param>
    public void SetChildrenVisibility(bool isVisible)
    {
        for(int i = 0; i < this.transform.childCount; i++)
        {
            //Get child
            var child = this.transform.GetChild(i).gameObject;

            //Set child visible
            child.SetActive(isVisible);
        }
    }
    #endregion
}
