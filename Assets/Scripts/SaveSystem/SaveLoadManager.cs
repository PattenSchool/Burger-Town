using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour, IObjectEvent
{
    #region Variables
    [SerializeField]
    private LevelData_SO currentLevelData;
    [SerializeField]
    private OverarchingGameData_SO gamedata;
    [SerializeField]
    private bool isLevel;
    public string saveName = "playerSave";

    public int currentMaxLevel;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        //Check if the playerprefs has the save name key
        if (PlayerPrefs.HasKey(saveName))
        {
            //if the max level exists, then add in
            currentMaxLevel = PlayerPrefs.GetInt(saveName, 1);
        }

        //Sets up if the player level is set
        if (isLevel == true)
        {
            //Set up player
            if (PlayerStatic.Player == null)
            {
                Debug.LogWarning("Player is not detected in scene");
                Debug.Break();
            }

            //Load level data and load player assets
            currentLevelData = FindLevelData(SceneManager.GetActiveScene().buildIndex);
            LoadPlayerAssets();

            //Save level
            SaveLevel();
        }
        //For menus and non-level areas
        else
        {
            //save level to max level
            if (PlayerPrefs.HasKey(saveName))
                currentMaxLevel = PlayerPrefs.GetInt(saveName, 1);
        }
    }
    #endregion

    #region Load Methods
    /// <summary>
    /// Loads a save (works with only one save currently)
    /// </summary>
    public void LoadSave()
    {
        //if (PlayerPrefs.GetInt(saveName) != 0)
        //{
        currentMaxLevel = PlayerPrefs.GetInt(saveName);
        SceneManager.LoadScene(currentMaxLevel);
        //}
    }
    #endregion

    #region Save Methods
    /// <summary>
    /// Reset max level to lowest level
    /// </summary>
    public void ResetMaxLevel()
    {
        currentMaxLevel = int.MaxValue;

        foreach (var levelData in gamedata.levels)
        {
            if (levelData.GetSceneIndex() <= currentMaxLevel)
            {
                currentMaxLevel = levelData.GetSceneIndex();
            }
        }

        PlayerPrefs.SetInt(saveName, currentMaxLevel);

        SceneManager.LoadScene(currentMaxLevel);
    }

    /// <summary>
    /// Save the level if max level if max level is bigger than the current level
    /// </summary>
    public void SaveLevel()
    {
        currentMaxLevel = PlayerPrefs.GetInt(saveName);
        if (currentMaxLevel <= SceneManager.GetActiveScene().buildIndex)
        {
            currentMaxLevel = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt(saveName, currentMaxLevel);
        }
    }

    /// <summary>
    /// Get the level data by searching in the build index
    /// </summary>
    /// <param name="currentLevelBuildIndex"></param>
    ///     The current level build index
    /// <returns></returns>
    ///     The current level data
    private LevelData_SO FindLevelData(int currentLevelBuildIndex)
    {
        foreach (LevelData_SO levelData in gamedata.levels)
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
    #endregion

    #region On Event Triggered
    /// <summary>
    /// Triggered if the event is triggered (such as a check point)
    /// </summary>
    public void IOnEventTriggered()
    {
        SaveLevel();
    }
    #endregion
    
    #region Player Loading
    /// <summary>
    /// Load the player assets
    /// </summary>
    private void LoadPlayerAssets()
    {
        //Loads the player's ammo
        LoadPlayerAmmo();
    }

    /// <summary>
    /// Load the player ammo to the player static object
    /// </summary>
    private void LoadPlayerAmmo()
    {
        //Make a list for the allowed bolts
        List<BoltTemplate> allowedBolts = new();

        //Set up the allowed bolts by checking each bolt
        for (int i = 0; i < gamedata.boltTemplates.Count; i++)
        {
            //Get the enum index (because index is stored in powers of 2)
            int enumIndex = (int)Mathf.Pow(2, i);

            //Get the bolt type
            BoltTypes lookingAtBoltType = (BoltTypes)enumIndex;

            //If bolt is allowed, then put it in the list
            if (currentLevelData.allowedBoltTypes.HasFlag(lookingAtBoltType))
            {
                //Get the bolt associated with that type
                BoltTemplate bolt = gamedata.boltTemplates[i];

                //Add in the allowed bolts
                allowedBolts.Add(bolt);
            }
        }

        //Trim list of allowed bolts
        allowedBolts.TrimExcess();

        //Set up allowed bolts
        PlayerStatic._shootScript.SetAllowedBolts(allowedBolts);
    }
    #endregion
}