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

    #region Dialogue Index and Get Method
    //!===========Variables and Properties===========!//
    [Header("Dialogue Index")]

    [Tooltip("The index of the text")]
    [SerializeField]
    private int dialogueIndex = 0;

    //!===================Methods====================!//
    /// <summary>
    /// Increases the text index by 1
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

    #region Sprite Related
    [Header("Sprite Related")]

    [Tooltip("The image holding the sprites")]
    [SerializeField]
    private Image expressionDisplay;

    [Tooltip("THe default color when the conversation is null")]
    [SerializeField]
    private Color defaultExpressionColor = Color.clear;
    #endregion

    #region Dialogue Display
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

        //Set text
        text.text = playerConversation.GetFormattedText(dialogueIndex);

        //TODO: Set expression
        var incomingExpressionSprite = playerConversation.GetExpressionSprite(dialogueIndex);
        if (incomingExpressionSprite == null)
        {
            expressionDisplay.color = defaultExpressionColor;
        }
        else
        {
            expressionDisplay.color = Color.white;
            expressionDisplay.sprite = incomingExpressionSprite;
        }
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

    #region Reset text elements
    //!===================Methods====================!//
    private void ResetTextElements()
    {
        dialogueIndex = 0;
        PlayerStatic.DeleteConversation();
        expressionDisplay.color = defaultExpressionColor;
        textBackground.SetActive(false);
    }
    #endregion
}
