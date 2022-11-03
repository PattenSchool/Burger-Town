using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour, IObjectEvent
{
    [SerializeField]
    private LevelData_SO currentLevelData;
    [SerializeField]
    private OverarchingGameData_SO gamedata;
    [SerializeField]
    private bool isLevel;
    public string saveName = "playerSave";

    public int currentMaxLevel;
    public void IOnEventTriggered()
    {
        SaveLevel();
        print(PlayerPrefs.GetInt(saveName));
    }
    public void ResetMaxLevel()
    {
        currentMaxLevel = 1000000;

        foreach(var levelData in gamedata.levels)
        {
            if (levelData.GetSceneIndex() <= currentMaxLevel)
            {
                currentMaxLevel = levelData.GetSceneIndex();
            }
        }

        PlayerPrefs.SetInt(saveName, currentMaxLevel);

        SceneManager.LoadScene(currentMaxLevel);
    }
    private void Awake()
    {
        if (PlayerPrefs.HasKey(saveName))
        {
            currentMaxLevel = PlayerPrefs.GetInt(saveName, 1);
        }

        if (isLevel == true)
        {
            if (PlayerStatic.Player == null)
            {
                Debug.LogWarning("Player is not detected in scene");
                Debug.Break();
            }

            currentLevelData = FindLevelData(SceneManager.GetActiveScene().buildIndex);
            LoadPlayerAssets();

            SaveLevel();
            print(PlayerPrefs.GetInt(saveName));
        }
        else
        {
            if (PlayerPrefs.HasKey(saveName))
                currentMaxLevel = PlayerPrefs.GetInt(saveName, 1);
        }
    }

    

    public void LoadSave()
    {
        //if (PlayerPrefs.GetInt(saveName) != 0)
        //{
            currentMaxLevel = PlayerPrefs.GetInt(saveName);
            SceneManager.LoadScene(currentMaxLevel);
        //}
    }

    public void SaveLevel()
    {
        currentMaxLevel = PlayerPrefs.GetInt(saveName);
        if (currentMaxLevel <= SceneManager.GetActiveScene().buildIndex)
        {
            currentMaxLevel = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt(saveName, currentMaxLevel);
        }
    }


    private LevelData_SO FindLevelData(int currentLevelBuildIndex)
    {
        foreach(LevelData_SO levelData in gamedata.levels)
        {
            if (levelData.GetSceneIndex() == currentLevelBuildIndex)
            {
                return levelData;
            }
        }

        Debug.LogError($"Name {SceneManager.GetActiveScene().name} is not in levelData");
        Debug.Break();
        return null;
    }

    #region Player Loading
    private void LoadPlayerAssets()
    {

        LoadPlayerAmmo();
    }
    private void LoadPlayerAmmo()
    {
        //Make a list for the allowed bolts
        List<BoltTemplate> allowedBolts = new();

        for (int i = 0; i < gamedata.boltTemplates.Count; i++)
        {
            //Get the enum index (because index is stored in powers of 2)
            int enumIndex = (int)Mathf.Pow(2, i);

            //Get the bolt type
            BoltTypes lookingAtBoltType = (BoltTypes)enumIndex;

            if (currentLevelData.allowedBoltTypes.HasFlag(lookingAtBoltType))
            {
                //Get the bolt associated with that type
                BoltTemplate bolt = gamedata.boltTemplates[i];

                allowedBolts.Add(bolt);
            }
        }

        allowedBolts.TrimExcess();

        PlayerStatic._shootScript.SetAllowedBolts(allowedBolts);
    }
    #endregion
}