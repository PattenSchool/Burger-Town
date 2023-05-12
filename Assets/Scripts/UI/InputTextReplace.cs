using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class InputTextReplace : MonoBehaviour
{
    #region Components
    [Header("Components")]

    [Tooltip("The UI element storing the text")]
    [SerializeField]
    private TMP_Text textUI;
    #endregion

    #region Stored Text
    [Header("Stored Text")]

    [Tooltip("The stored reference text")]
    [SerializeField]
    private string storedText = "";
    #endregion

    #region Unity Methods
    private void Update()
    {
        textUI.text = InputStatic.InputNames.TranslateTextToInstruction(storedText, '`');
    }

    private void OnEnable()
    {
        if (textUI == null)
            textUI = GetComponent<TMP_Text>();

        storedText = textUI.text;
    }

    private void OnDisable()
    {
        textUI.text = storedText;
    }
    #endregion
}
