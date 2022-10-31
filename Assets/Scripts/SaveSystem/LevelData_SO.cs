using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Level Data", menuName = "Game Data/New Level Data SO")]
public class LevelData_SO : ScriptableObject
{
    [System.Flags]
    public enum BoltTypes
    {
        normalBolt = 1,
        stickyBolt = 2,
        recoilBolt = 4
    }

    [Header("Bolt Related")]
    public BoltTypes allowedBoltTypes;
}
