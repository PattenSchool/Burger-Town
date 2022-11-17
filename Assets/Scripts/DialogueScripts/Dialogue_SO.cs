using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "DIalogueData/Dialogue")]
public class Dialogue_SO : ScriptableObject
{
    public enum SpeakerType
    {
        None,
        Player,
        MayorMayonase,
        Boss
    }

    public SpeakerType type;

    public string text;


}
