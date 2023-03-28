using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Saves Data", menuName = "Save Data/Overarching save data")]
public class OverarchingSavesData_SO : ScriptableObject
{
    public List<SaveData_SO> saves;
}
