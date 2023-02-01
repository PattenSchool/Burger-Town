using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDisplayText : MonoBehaviour, IObjectEvent
{
    public Conversation_SO conversation;

    public void IOnEventTriggered()
    {
        PlayerStatic.OverrideConversation(conversation);
    }
}
