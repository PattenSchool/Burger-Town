using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

<<<<<<< Updated upstream:Assets/Scripts/SaveSystem/LevelData_SO.cs
[CreateAssetMenu(fileName = "new Level Data", menuName = "Level Data/New Level Data SO")]
=======

[System.Flags]
public enum BoltTypes
{
    normalBolt = 1,
    stickyBolt = 2,
    recoilBolt = 4
}
[CreateAssetMenu(fileName = "new Level Data", menuName = "Game Data/New Level Data SO")]
>>>>>>> Stashed changes:Assets/Scripts/SaveSystem/ScriptsForData/LevelData_SO.cs
public class LevelData_SO : ScriptableObject
{
    [SerializeField]
    private string scene;
    

    [Header("Bolt Related")]
    public BoltTypes allowedBoltTypes;
}
