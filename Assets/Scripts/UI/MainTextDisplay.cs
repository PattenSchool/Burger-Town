using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Used to display the ui of the player
/// </summary>
public class MainTextDisplay : MonoBehaviour
{
    #region Text Components
    //!===========Variables and Properties===========!//
    [Header("Text Components")]

    [Tooltip("The background the text sets on")]
    [SerializeField]
    private GameObject textBackground;

    [Tooltip("The text to be displayed")]
    [SerializeField]
    private TMP_Text text;

    [Tooltip("The text being looked at")]
    [SerializeField]
    private int textDisplayIndex = 0;
    #endregion

    #region Expression Displays
    [Header("Expression UI elements")]

    [Tooltip("The Image containing where the expressions will be displayed")]
    [SerializeField]
    private Image expressionsImage;
    #endregion

    #region Text Index and Get Method
    //!===========Variables and Properties===========!//
    [Header("Text Index")]

    [Tooltip("The index displaying the dialogue")]
    [SerializeField]
    private int dialogueIndex = 0;

    //!===================Methods====================!//
    /// <summary>
    /// Increases the index by 1
    /// </summary>
    public void IncrementDialogueIndex()
    {
        dialogueIndex++;
    }

    /// <summary>
    /// Gets the text index
    /// </summary>
    /// <returns></returns>
    public int GetDialogueIndex()
    {
        return dialogueIndex;
    }

    /// <summary>
    /// Decreases the text index by 1
    /// </summary>
    public void DecrementDialogueIndex()
    {
        dialogueIndex--;
    }
    #endregion

    #region Text Display
    //!===================Methods====================!//
    private void DisplayConversation()
    {
        //Get's a temp conversation
        Conversation_SO playerConversation = new();

        //Safe gaurd if player doesn't have a conversation
        if (!PlayerStatic.HasConversation())
            return;
        else
            playerConversation = PlayerStatic.Conversation;

        //If text conversation is over the allowed amount, then safegaurd
        if (dialogueIndex >= playerConversation.ConversationLength)
        {
            ResetTextElements();
            return;
        }

        //Set text and text background active if not active already
        if (textBackground.activeInHierarchy == false)
            textBackground.SetActive(true);

        //Set Dialogue ELements
        text.text = playerConversation.GetFormattedText(dialogueIndex);
        expressionsImage.sprite = playerConversation.GetExpressionSprite(dialogueIndex);
    }
    #endregion

    #region Unity Methods
    private void Start()
    {
        PlayerStatic.DeleteConversation();
    }

    private void LateUpdate()
    {
        DisplayConversation();
    }
    #endregion

    #region Reset elements
    //!===================Methods====================!//
    private void ResetTextElements()
    {
        dialogueIndex = 0;
        PlayerStatic.DeleteConversation();
        textBackground.SetActive(false);
    }
    #endregion
}
