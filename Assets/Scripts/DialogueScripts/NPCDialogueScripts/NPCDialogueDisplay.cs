using UnityEngine;
using TMPro;
using System.ComponentModel;
using UnityEngine.MathExtensions;

/// <summary>
/// Displays the dialogue of NPCs
/// </summary>
public class NPCDialogueDisplay : MonoBehaviour
{
    #region Display
    //!===========Variables and Properties===========!//
    [Header("UI Elements")]

    [Tooltip("The Text display carrying the text")]
    [SerializeField]
    protected TMP_Text textDisplay;

    //!===================Methods====================!//
    /// <summary>
    /// Displays a dialogue if a conversation exists,
    ///     else it will print a dialogue
    /// </summary>
    public virtual void DisplayDialogue()
    {
        if (conversation != null)
            textDisplay.text = conversation.GetFormattedText(dialogueIndex);
        else
            print($"Conversation at {this.gameObject} does not have a conversation for a dialogue" +
                $" to diaplay");
    }

    /// <summary>
    /// Displays a dialogue with an index
    /// </summary>
    /// <param name="index"></param>
    ///     The index to specify a dialogue is set
    public void DisplayDialogue(int index)
    {
        //Sets the index
        SetDialogueIndex(index);

        //Display the dialogue
        textDisplay.text = conversation.GetFormattedText(index);
    }

    /// <summary>
    /// Resets the dialogue display
    /// </summary>
    public void ResetDisplay()
    {
        textDisplay.text = "";
    }
    #endregion

    #region Conversation Related
    //!===========Variables and Properties===========!//
    [Header("Conversation Elements")]

    [Tooltip("A conversation that this display is holding")]
    [SerializeField]
    protected Conversation_SO conversation;

    //!===================Methods====================!//
    /// <summary>
    /// Set's a new conversation
    /// </summary>
    /// <param name="newConversation"></param>
    ///     Get's the conversation
    public void OverrideConversation(Conversation_SO newConversation)
    {
        //Set the dialoge
        conversation = newConversation;
    }

    /// <summary>
    /// Deletes the conversation currently being held
    /// </summary>
    public void DeleteConversation()
    {
        conversation = null;
    }
    #endregion

    #region Index Related
    //!===========Variables and Properties===========!//
    [Header("Indexer Elements")]

    [Tooltip("The index of the dialogue")]
    [SerializeField, Min(0)]
    protected int dialogueIndex = 0;
    public int DialogueIndex
    {
        get { return dialogueIndex; }
        set { dialogueIndex = value; }
    }

    //!===================Methods====================!//
    public void ResetDialogueIndex()
    {
        dialogueIndex = 0;
    }

    public void IncrementDialogueIndex()
    {
        dialogueIndex++;
    }

    public void DecrementDialogueIndex()
    {
        dialogueIndex--;
    }

    public void SetDialogueIndex(int newIndex)
    {
        dialogueIndex = newIndex;
    }
    #endregion

    #region Unity Methods
    private void Awake()
    {
        ResetDialogueIndex();
    }
    #endregion
}
