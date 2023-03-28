using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains the overarching game data pertaining to levels
/// </summary>
[CreateAssetMenu(fileName = "New File Data", menuName = "Game Data/Overarching game data")]
public class OverarchingGameData_SO : ScriptableObject
{
    public List<LevelData_SO> levels;
    public List<BoltTemplate> boltTemplates;
}
