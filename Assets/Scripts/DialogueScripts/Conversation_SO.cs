using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Used to store conversational, dialogue, or narrorational elements
/// </summary>
[CreateAssetMenu(fileName = "New Conversation", menuName = "DIalogueData/Convo")]
[System.Serializable]
[SerializeField]
public class Conversation_SO : ScriptableObject
{
    #region Dialogue Variables
    //Dialogue Struct
    [System.Serializable]
    public class Dialogue
    {
        #region Sprites
        //[Header("Sprite Elements")]

        //[Tooltip("The facial expression sprite that can be displayed")]
        public Sprite expressionSprite;
        #endregion

        #region Written Elements
        //[Header("Written Elements")]

        //[Tooltip("The type of narrorator being displayed")]
        public NarrationType Narrorator;
        public enum NarrationType
        {
            None,
            Player,
            Mayor_Mayonnaise,
            Boss
        }

        //[Tooltip("The text of the dialogue")]
        public string text;
        #endregion
    }

    //Dialogue Array
    public List<Dialogue> dialogues = new List<Dialogue>();
    #endregion

    #region Dialogues Edit Methods
    /// <summary>
    /// Allows someone to add in a new dialogue to put into the text system
    /// </summary>
    public void AddLastDialogue()
    {
        if (dialogues == null)
        {
            dialogues = new List<Dialogue>();
        }

        dialogues.Add(new Dialogue());
    }

    /// <summary>
    /// Allows to remove the last dialogue
    /// </summary>
    public void RemoveLastDialogue()
    {
        if (dialogues == null)
        {
            return;
        }

        dialogues.RemoveAt(dialogues.Count - 1);
    }

    /// <summary>
    /// Removes a dialogue at a given index (starting at 0)
    /// </summary>
    /// <param name="index"></param>
    public void RemoveDialogueAtIndex(int index)
    {
        if (dialogues == null || index >= dialogues.Count || index < 0)
        {
            Debug.Log("Index Error On Conversation");
            return;
        }

        dialogues.RemoveAt(index);

    }
    #endregion

    #region Sprite Access
    /// <summary>
    /// Get's the sprite on a certain index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Sprite GetExpressionSprite(int index)
    {
        if (dialogues.Count >= 0)
        {
            //Return slected dialogue
            var dialogue = dialogues[index];

            return dialogue.expressionSprite;
        }

        return null;
    }
    #endregion

    #region Return Text
    public string GetFormattedText(int index)
    {
        if (dialogues.Count >= 0)
        {
            //Get the dialogue
            var dialogue = dialogues[index];

            //Set up default string
            string text = "";

            //Set string with text and narroration
            if (dialogue.Narrorator == Dialogue.NarrationType.None)
                text = $"{dialogue.text}";
            else
                text = $"{dialogue.Narrorator}: {dialogue.text}";

            return text;
        }
        else
        {
            return "";
        }

    }

    public int ConversationLength
    {
        get { return dialogues.Count; }
    }
    #endregion
}
