using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainTextDisplay : MonoBehaviour
{
    #region Text Components
    [Header("Text Components")]

    [Tooltip("The background the text sets on")]
    [SerializeField]
    private GameObject textBackground;

    [Tooltip("The text to be displayed")]
    [SerializeField]
    private TMP_Text text;
    #endregion

    #region Timer Variables
    [Header("TImer variables")]

    [Tooltip("The timer for display in seconds")]
    [SerializeField, Min(0.1f)]
    private float timer = 0f;
    #endregion

    #region Conversation Strings
    [Header("Conversation strings")]

    [SerializeField]
    private string[] textText = new string[0];

    private Conversation_SO analyzedConvo;
    #endregion

    #region Unity Methods
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
    private IEnumerator DisplayDialogue()
    {
        analyzedConvo = PlayerStatic.Conversation;

        MakeTextArray(PlayerStatic.Conversation);

        for(int i = 0; i < textText.Length; i++)
        {

            if (!PlayerStatic.HasConversation())
            {
                yield break;
            }

            text.text = textText[i];

            yield return new WaitForSeconds(timer);

        }

        PlayerStatic.DeleteConversation();

        ResetTextElements();

        textText = new string[0];
    }

    private void MakeTextArray(Conversation_SO tempConvo)
    {
        textText = new string[tempConvo.ConversationLength];


        for (int i = 0; i < tempConvo.ConversationLength; i++)
        {
            textText[i] = tempConvo.GetFormattedText(i);
        }
    }
    #endregion

    #region Reset Text Elements
    private void ResetTextElements()
    {
        StopCoroutine(DisplayDialogue());
        textBackground.SetActive(false);
        analyzedConvo = null;
    }

    #endregion
}
