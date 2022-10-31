using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField]
    private LevelData_SO levelData;
    [SerializeField]
    private OverarchingGameData_SO gamedata;
    private void Awake()
    {
        for (int i = 0; i < gamedata.boltTemplates.Count; i++)
        {
            //Get the enum index (because index is stored in powers of 2)
            int enumIndex = (int)Mathf.Pow(2, i);

            //Get the bolt type
            BoltTypes lookingAtBoltType = (BoltTypes)enumIndex;

            if (levelData.allowedBoltTypes.HasFlag(lookingAtBoltType))
            {
                //Get the bolt associated with that type
                GameObject bolt = gamedata.boltTemplates[i - 1].gameObject;

                //Print the name
                print(bolt.name);
            }
        }
    }

}