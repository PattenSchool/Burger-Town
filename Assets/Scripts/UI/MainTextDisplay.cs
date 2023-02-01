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

    //#region Timer Components
    ////!===========Variables and Properties===========!//
    //[Header("TImer variables")]

    //[Tooltip("The timer for display in seconds")]
    //[SerializeField, Min(0.1f)]
    //private float timer = 0f;
    //#endregion

    #region Conversation Strings
    //!===========Variables and Properties===========!//
    [Header("Conversation strings")]

    [SerializeField]
    private string[] conversationStringHolder = new string[0];

    private Conversation_SO analyzedConvo;
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

    #region Unity Methods
    //!===========Variables and Properties===========!//
    private void Awake()
    {
        PlayerStatic.DeleteConversation();
        ResetTextElements();
    }

    private void Update()
    {
        if (!PlayerStatic.HasConversation())
        {
            ResetTextElements();
        }
        else if (!textBackground.activeInHierarchy && PlayerStatic.HasConversation())
        {
            textBackground.SetActive(true);
            StartCoroutine(DisplayDialogue());
        }

        else if (PlayerStatic.Conversation != analyzedConvo)
        {
            StopAllCoroutines();
            ResetTextElements();
            textBackground.SetActive(true);
            StartCoroutine(DisplayDialogue());
        }
    }
    #endregion

    #region Edit Text
    //!===================Methods====================!//
    /// <summary>
    /// Displays the dialogue
    /// </summary>
    /// <returns></returns>
    private IEnumerator DisplayDialogue()
    {
        //Turn a conversation into a more flexable text array
        analyzedConvo = PlayerStatic.Conversation;
        MakeTextArray(PlayerStatic.Conversation);

        //Display text one by one
        while(PlayerStatic.HasConversation() && textIndex < conversationStringHolder.Length)
        {
            //if (!PlayerStatic.HasConversation())
            //{
            //    yield break;
            //}
            text.text = conversationStringHolder[textIndex];

            yield return null;
            //Wait after a specified period of time
            //yield return new WaitForSeconds(timer);
        }

        //Clean up conversation elements
        PlayerStatic.DeleteConversation();
        ResetTextElements();
        conversationStringHolder = new string[0];
    }

    private void MakeTextArray(Conversation_SO tempConvo)
    {
        conversationStringHolder = new string[tempConvo.ConversationLength];


        for (int i = 0; i < tempConvo.ConversationLength; i++)
        {
            conversationStringHolder[i] = tempConvo.GetFormattedText(i);
        }
    }
    #endregion

    #region Reset Text Elements
    //!===================Methods====================!//
    /// <summary>
    /// Resets all of the text elements
    /// </summary>
    private void ResetTextElements()
    {
        StopCoroutine(DisplayDialogue());
        textBackground.SetActive(false);
        analyzedConvo = null;
        textIndex = 0;
    }
    #endregion
}
