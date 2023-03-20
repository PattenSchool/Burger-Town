using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class NPCDialogueTrigger : MonoBehaviour
{
    #region Canvas Dialogue
    //!===========Variables and Properties===========!//
    [Header("Display Elements")]

    [Tooltip("The display")]
    [SerializeField]
    private NPCDialogueDisplay dialogueDisplay;
    #endregion

    #region Unity Methods
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == PlayerStatic.PlayerTag)
        {
            dialogueDisplay.PlayConversation();
        }
    }
    #endregion
}
