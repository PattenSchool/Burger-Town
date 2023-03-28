//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

//public class DialogueManager : MonoBehaviour
//{
//    public TMP_Text text;
//    private Conversation_SO curConversation;
//    public static DialogueManager instance;
//    private int curDialogue;
//    private void Awake()
//    {
//        if (DialogueManager.instance == null)
//        {
//            DialogueManager.instance = this;
//        }
//        else if (DialogueManager.instance != this)
//        {
//            Destroy(this);
//        }
//    }

//    public void SetConversation(Conversation_SO conversation)
//    {
//        curConversation = conversation;
//        curDialogue = -1;
//        ProceedToNextDialogue();
//    }

//    public void ProceedToNextDialogue()
//    {
//        if (curConversation == null)
//        {
//            return;
//        }

//        curDialogue++;

//        if (curDialogue > curConversation.dialogues.Length - 1)
//        {
//            Debug.Log("Conversation over");
//            return;
//        }

//        text.text = curConversation.dialogues[curDialogue].text;
//    }


//}
