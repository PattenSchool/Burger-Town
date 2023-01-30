using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDialogueTrigger : MonoBehaviour
{
    #region Variables
    [Tooltip("The conversation set inside the dialogue box")]
    [SerializeField]
    private Conversation_SO conversation;
    #endregion

    #region Unity Methods
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == PlayerStatic.PlayerTag)
        {
            PlayerStatic.OverrideConversation(conversation);
        }
    }
    #endregion
}
