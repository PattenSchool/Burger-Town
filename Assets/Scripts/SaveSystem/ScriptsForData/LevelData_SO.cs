using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Flags]
public enum BoltTypes
{
    normalBolt = 1,
    stickyBolt = 2,
    recoilBolt = 4
}
[CreateAssetMenu(fileName = "new Level Data", menuName = "Game Data/New Level Data SO")]
public class LevelData_SO : ScriptableObject
{
    [SerializeField]
    private int sceneIndex;
    

    [Header("Bolt Related")]
    public BoltTypes allowedBoltTypes;

    public int GetSceneIndex()
    {
        return sceneIndex;
    }
}
