using System.Collections;
using UnityEngine;
using TMPro;

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

    #region Text Index and Get Method
    //!===========Variables and Properties===========!//
    [Header("Text Index")]

    [Tooltip("The index of the text")]
    [SerializeField]
    private int textIndex = 0;

    //!===================Methods====================!//
    /// <summary>
    /// Increases the text index by 1
    /// </summary>
    public void IncrementTextIndex()
    {
        textIndex++;
    }

    /// <summary>
    /// Gets the text index
    /// </summary>
    /// <returns></returns>
    public int GetTextIndex()
    {
        return textIndex;
    }

    /// <summary>
    /// Decreases the text index by 1
    /// </summary>
    public void DecrementTextIndex()
    {
        textIndex--;
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
        if (textIndex >= playerConversation.ConversationLength)
        {
            ResetTextElements();
            return;
        }

        //Set text and text background active if not active already
        if (textBackground.activeInHierarchy == false)
            textBackground.SetActive(true);

        //Set text
        text.text = playerConversation.GetFormattedText(textIndex);
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
        textIndex = 0;
        PlayerStatic.DeleteConversation();
        textBackground.SetActive(false);
    }
    #endregion
}
