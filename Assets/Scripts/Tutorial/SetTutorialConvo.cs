using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTutorialConvo : MonoBehaviour
{
    [SerializeField]
    private Conversation_SO convo;

    private void OnCollisionEnter(Collision collision)
    {
        SetConvo();
    }
    private void OnTriggerEnter(Collider other)
    {
        SetConvo();
    }

    public void SetConvo()
    {
        PlayerStatic.OverrideConversation(convo);
    }
}
