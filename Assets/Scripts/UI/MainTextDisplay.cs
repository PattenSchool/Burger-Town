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

    #region Unity Methods
    private void Update()
    {
        bool isReady =
            PlayerStatic.TextQueue.Count > 0 &&
            text.text == "" &&
            PlayerStatic.TextQueue != null;


        if (isReady)
        {

            if (!textBackground.activeInHierarchy)
            {
                textBackground.SetActive(true);
            }
            StartCoroutine(SetDisplay());
        }


        if (PlayerStatic.TextQueue.Count == 0)
        {
            textBackground.SetActive(false);
        }
    }
    #endregion

    #region Text Coroutine
    private IEnumerator SetDisplay()
    {
        while(PlayerStatic.TextQueue.Count > 0)
        {
            text.text = PlayerStatic.TextQueue.Dequeue();
            yield return null;
        }
    }
    #endregion
}
