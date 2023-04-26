using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDialogueTrigger : MonoBehaviour
{
    #region Variables
    [Tooltip("The conversation set inside the dialogue box")]
    [SerializeField]
    private Conversation_SO conversation;

    [Tooltip("Choose if despawns after use")]
    [SerializeField]
    private bool isDespawnAfterUse;
    #endregion

    #region Unity Methods
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == PlayerStatic.PlayerTag)
        {
            PlayerStatic.OverrideConversation(conversation);
            this.gameObject.SetActive(!isDespawnAfterUse);
        }
    }
    #endregion
}
