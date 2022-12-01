using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Used to store conversational, dialogue, or narrorational elements
/// </summary>
[CreateAssetMenu(fileName = "New Conversation", menuName = "DIalogueData/Convo")]
[System.Serializable]
[SerializeField]
public class Conversation_SO : ScriptableObject
    //, ISerializationCallbackReceiver
{
    #region Dialogue Variables
    //Dialogue Struct
    [System.Serializable]
    public class Dialogue
    {
        public enum NarrationType
        {
            None,
            Player,
            MayorMayonase,
            Boss
        }
        //adsf
        public NarrationType Narrorator;
        public string text;
    }

    //Dialogue Array
    public List<Dialogue> dialogues = new List<Dialogue>();
    #endregion

    #region Dialogues Edit Methods
    /// <summary>
    /// Allows someone to add in a new dialogue to put into the text system
    /// </summary>
    public void AddLastDialogue()
    {
        if(dialogues == null)
        {
            dialogues = new List<Dialogue>();
        }

        dialogues.Add(new Dialogue());

        /*
        if (dialogues.Count == 0)
        {
            dialogues = new Dialogue[1];
        }
        else
        {
            var tempList = dialogues.ToList();
            tempList.Add(tempList[tempList.Count - 1]);
            tempList.TrimExcess();
            dialogues = tempList.ToArray();
        }
        */
    }

    /// <summary>
    /// Allows to remove the last dialogue
    /// </summary>
    public void RemoveLastDialogue()
    {
        if(dialogues == null)
        {
            return;
        }

        dialogues.RemoveAt(dialogues.Count - 1);

        /*
        var tempList = dialogues.ToList();
        if (tempList.Count > 0)
            tempList.RemoveAt(tempList.Count - 1);
        dialogues = tempList.ToArray();
        */
    }

    public void RemoveDialogueAtIndex(int index)
    {
        if (dialogues == null || index >= dialogues.Count || index < 0)
        {
            Debug.Log("Index Error On Conversation");
            return;
        }

        dialogues.RemoveAt(index);

        /*
        if (dialogues.Length > 0)
        {
            var tempList = dialogues.ToList();
            tempList.RemoveAt(index);
            tempList.TrimExcess();
            dialogues = tempList.ToArray();
        }
        else
        {
            Debug.Log("No more dialogue elements");
        }
        */
        
    }
    #endregion

    #region Return Text
    public string GetFormattedText(int index)
    {
        if (dialogues.Count > 0)
        {
            //Get the dialogue
            var dialogue = dialogues[index];

            //Set up default string
            string text = "";

            //Set string with text and narroration
            if (dialogue.Narrorator == Dialogue.NarrationType.None)
                text = $"{dialogue.text}";
            else
                text = $"{dialogue.Narrorator}: {dialogue.text}";

            return text;
        }
        else
        {
            return "";
        }
        
    }

    public int ConversationLength
    {
        get { return dialogues.Count; }
    }
    #endregion

    /// <summary>
    /// !!DO NOT TOUCH!!
    /// </summary>
    //#region Serialiation Methods and Attributes
    ////Used to save narration types
    //[SerializeField, HideInInspector]
    //private int[] dialogueNarroratorSaves = new int[0];

    ////Used to save the texts
    //[SerializeField, HideInInspector]
    //private string[] dialogueTextSaves = new string[0];

    ///// <summary>
    ///// Called before saving
    ///// </summary>
    //public void OnBeforeSerialize()
    //{
    //    //Set array lengths
    //    dialogueNarroratorSaves = new int[dialogues.Length];
    //    dialogueTextSaves = new string[dialogues.Length];

    //    //Save array contents
    //    for (int i = 0; i < dialogues.Length; i++)
    //    {
    //        dialogueNarroratorSaves[i] = (int)dialogues[i].Narrorator;
    //        dialogueTextSaves[i] = dialogues[i].text;
    //    }
    //}

    ///// <summary>
    ///// Called to reload data
    ///// </summary>
    //public void OnAfterDeserialize()
    //{
    //    //Set dialogue length
    //    dialogues = new Dialogue[dialogueNarroratorSaves.Length];

    //    //Reload dialogue content
    //    for (int i = 0; i < dialogueNarroratorSaves.Length; i++)
    //    {
    //        dialogues[i].Narrorator = (Dialogue.NarrationType)dialogueNarroratorSaves[i];
    //        dialogues[i].text = dialogueTextSaves[i];
    //    }
    //}
    //#endregion
}

#if UNITY_EDITOR
/// <summary>
/// Used to design the Conversation_SO inspector
/// </summary>
[CustomEditor(typeof(Conversation_SO)), CanEditMultipleObjects]
public class ConversationEditor : Editor
{
    #region Conversation Dialogue Inspector Var
    //Used to show the dialogue collapsables
    protected bool showDialogues = true;

    //Used to pop a dialogue from an index
    protected int dialogueIndexPop = 0;
    #endregion

    /*
#region Inspector Methods
/// <summary>
/// Set up the inspector
/// </summary>
///
public override void OnInspectorGUI()
{
    //Get conversation target
    Conversation_SO conversation = (Conversation_SO)target;

    #region Dialogue Element Editor
    //Show if dialogues is colapsed or not
    showDialogues = EditorGUILayout.Foldout(showDialogues, "Conversation Elements");

    //If dropdown is true, then display
    if (showDialogues)
    {
        //For each element of dialogues in the conversation
        for (int i = 0; i < conversation.dialogues.Count; i++)
        {
            EditorGUILayout.LabelField($"Dialogue {i}");

            //Edit the text of a dialogue
            conversation.dialogues[i].text =
                EditorGUILayout.TextField(conversation.dialogues[i].text);

            //Edit the narrorator of the dialogue
            conversation.dialogues[i].Narrorator =
                (Conversation_SO.Dialogue.NarrationType)
                EditorGUILayout.EnumPopup(conversation.dialogues[i].Narrorator);
        }
    }
    #endregion


    #region Dailogue Length Edit
    EditorGUILayout.BeginHorizontal();

    if (GUILayout.Button("+"))
    {
        conversation.AddLastDialogue();
    }

    if (GUILayout.Button("-"))
    {
        conversation.RemoveLastDialogue();
    }

    EditorGUILayout.EndHorizontal();
    #endregion

    #region Dialogue Pop Edit
    EditorGUILayout.BeginHorizontal();

    dialogueIndexPop = 
        EditorGUILayout.IntSlider(dialogueIndexPop, 0, 
            conversation.dialogues.Count <= 1? 0: conversation.dialogues.Count - 1);

    EditorGUILayout.EndHorizontal();

    if (GUILayout.Button("Remove At Index") && conversation.dialogues.Count > 0)
    {
        conversation.RemoveDialogueAtIndex(dialogueIndexPop);
    }
    #endregion
}
#endregion
    */
}
#endif
