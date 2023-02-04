using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialoguetrigger : MonoBehaviour
{
    #region Variables
    //!===========Variables and Properties===========!//
    [Header("Variables")]

    [Tooltip("The display element")]
    [SerializeField]
    private TimedSequentialNPCDisplay convoDisplay;

    [Tooltip("Conversation to display")]
    [SerializeField]
    private Conversation_SO npcConversation;
    #endregion

    #region Unity Methods
    private void OnTriggerEnter(Collider other)
    {
        //Filter out anything that isn't a player
        if (other.tag != PlayerStatic.PlayerTag)
            return;

        //TODO: Set the conversation
        convoDisplay.OverrideConversation(npcConversation);

        //TODO: Display the Conversation
        StartCoroutine(convoDisplay.DisplayTimedDialogue());
    }
    #endregion
}
