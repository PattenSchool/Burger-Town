using JetBrains.Annotations;
using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveLoadManager : MonoBehaviour, IObjectEvent
{
    #region Variables
    [SerializeField]
    private LevelData_SO currentLevelData;
    [SerializeField]
    private OverarchingGameData_SO gamedata;
    [SerializeField]
    private bool isLevel;

    // Remove Later
    public string saveName = "playerSave";
    //[SerializeField]
    private SaveData_SO currentSaveData;
    [SerializeField]
    private OverarchingSavesData_SO saveData;
    public string currentSaveName = "currentSave";

    public RectTransform scrollParent;
    public UnityEngine.UI.Button buttonPrefab;
    private List<Button> levelButtons = new List<Button>();

    public RectTransform loadSaveParent;
    public RectTransform saveParent;


    public int currentMaxLevel;
    #endregion

    #region Unity Methods
    private void Start()
    {
        // Load max level from prefs into SaveData_SO's
        UpdateSaveData();

        // Possibly move into separate method like UpdateSaveData()
        // Get current save from save file and array
        if (PlayerPrefs.HasKey(currentSaveName))
        {
            int currentSaveIndex = PlayerPrefs.GetInt(currentSaveName);
            foreach (SaveData_SO save in saveData.saves)
            {
                if (save.saveIndex == currentSaveIndex)
                {
                    currentSaveData = save;
                }
            }
        }

        // Instantiate levels into level menu
        SetupLevelMenu();

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
    }
    #endregion

    #region Load Methods
    /// <summary>
    /// Loads a save
    /// </summary>
    /// 
    public void LoadSave (int saveIndex)
    {
        foreach (SaveData_SO save in saveData.saves)
        {
            if (save.saveIndex == saveIndex)
            {
                currentSaveData = save;
                PlayerPrefs.SetInt(currentSaveName, currentSaveData.saveIndex);
                currentMaxLevel = currentSaveData.maxLevel;
            }
        }
    }


    public void UpdateSaveData()
    {
        foreach (SaveData_SO save in saveData.saves)
        {
            if (PlayerPrefs.HasKey(save.name + save.saveIndex))
            {
                save.maxLevel = PlayerPrefs.GetInt(save.name + save.saveIndex);
            }
            else
            {
                save.maxLevel = 0;
            }
        }
    }

    public void SetupLevelMenu()
    {
        for (int i = 0; i < gamedata.levels.Count; i++)
        {

            Button buttonInstance = Instantiate(buttonPrefab, scrollParent);

            buttonInstance.GetComponentInChildren<TextMeshProUGUI>().text = gamedata.levels[i].name;

            levelButtons.Add(buttonInstance);
        }

        scrollParent.sizeDelta = new Vector2(scrollParent.sizeDelta.x, levelButtons.Count * 105f);
    }

    public void DisplayLevels()
    {
        foreach (Button buttonInstance in levelButtons)
        {
            if (buttonInstance.gameObject.activeInHierarchy)
            {
                buttonInstance.gameObject.SetActive(false);
            }
        }
        

        for (int i = 0; i < levelButtons.Count; i++)
        {
            if (gamedata.levels[i].GetSceneIndex() <= currentSaveData.maxLevel)
            {
                levelButtons[i].gameObject.SetActive(true);
                if (i > 0)
                {
                    levelButtons[i].transform.position = new Vector3(levelButtons[i].transform.position.x,
                        levelButtons[i - 1].transform.position.y - 105f, levelButtons[i].transform.position.z);
                }
                else
                {
                    levelButtons[i].transform.position = new Vector3(levelButtons[i].transform.position.x,
                        scrollParent.transform.position.y - 55f, levelButtons[i].transform.position.z);
                }
            }
            else if (currentSaveData.maxLevel <= 0)
            {
                levelButtons[0].gameObject.SetActive(true);
            }
        }
    }

    public void DisplaySaves()
    {
        RectTransform tempTransform;

        if (saveParent.gameObject.activeInHierarchy)
        {
            tempTransform = saveParent;
        }
        else
        {
            tempTransform = loadSaveParent;
        }

        Button[] buttons = tempTransform.GetComponentsInChildren<Button>();

        List<Button> buttonsList = new List<Button>();

        foreach (Button button in buttons)
        {
            if (button.gameObject.tag != "Ignore")
            {
                buttonsList.Add(button);
            }
        }

        for (int i = 0; i < buttonsList.Count; i++)
        {
            string buttonName = "Save " + buttonsList[i].name + ": " +
                " Level: " + saveData.saves[i].maxLevel.ToString();


            buttonsList[i].GetComponentInChildren<TextMeshProUGUI>().text = buttonName;
        }
    }

    public void ClearSave(int index)
    {
        saveData.saves[index - 1].maxLevel = 0;

        PlayerPrefs.SetInt(saveData.saves[index - 1].name + saveData.saves[index - 1].saveIndex, 0);

        foreach (SaveData_SO save in saveData.saves)
        {
            if (save.saveIndex == index)
            {
                PlayerPrefs.SetInt(save.name + save.saveIndex, 0);
                save.maxLevel = 0;
            }
        }

        DisplaySaves();
    }


    public void LoadSceneFromData(Button button)
    {
        foreach (LevelData_SO level in gamedata.levels)
        {
            if (level.name == button.GetComponentInChildren<TextMeshProUGUI>().text)
            {
                SceneManager.LoadScene(level.GetSceneIndex());
            }
        }
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
    /// Save the level if the current save's max level is lower than or equal to the current level
    /// </summary>
    public void SaveLevel()
    {
        currentMaxLevel = currentSaveData.maxLevel;
        if (currentMaxLevel <= SceneManager.GetActiveScene().buildIndex)
        {
            currentMaxLevel = SceneManager.GetActiveScene().buildIndex;
            currentSaveData.maxLevel = currentMaxLevel;
            PlayerPrefs.SetInt(currentSaveData.name + currentSaveData.saveIndex, currentMaxLevel);
        }
    }

    public void SaveLevel(int index)
    {
        foreach (SaveData_SO save in saveData.saves)
        {
            if (save.saveIndex == index)
            {
                if (save == currentSaveData)
                {
                    if (save.maxLevel <= SceneManager.GetActiveScene().buildIndex)
                    {
                        save.maxLevel = SceneManager.GetActiveScene().buildIndex;
                        currentMaxLevel = save.maxLevel;
                        PlayerPrefs.SetInt(save.name + save.saveIndex, save.maxLevel);
                    }
                }
                else
                {
                    save.maxLevel = SceneManager.GetActiveScene().buildIndex;
                    currentMaxLevel = save.maxLevel;
                    PlayerPrefs.SetInt(save.name + save.saveIndex, save.maxLevel);
                }
            }
        }

        DisplaySaves();
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