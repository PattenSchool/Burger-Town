using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public RectTransform QuestDisplay;

    public RectTransform ActiveBurgerSprite;

    public RectTransform InactiveBurgerSprite;

    private List<RectTransform> ActiveBurgerSprites = new List<RectTransform>();

    private List<RectTransform> InactiveBurgerSprites = new List<RectTransform>();

    private List<AbstractObjective> currentObjectives = new List<AbstractObjective>();

    private List<RectTransform> ObjectiveDisplays = new List<RectTransform>();

    [SerializeField]
    public List<Quest> currentQuests;

    public bool showLoading = false;

    public bool showLeft = false;

    private int objsNotComplete;

    private List<Quest> displayedQuests = new List<Quest> ();

    private bool isBurgerDisplayed = false;

    private ObjectiveObtainBurger objective = null;

    void Start()
    {

        foreach (RectTransform child in ActiveBurgerSprite.GetComponentsInChildren<RectTransform>())
        {
            ActiveBurgerSprites.Add(child);
        }

        foreach (RectTransform child in InactiveBurgerSprite.GetComponentsInChildren<RectTransform>())
        {
            InactiveBurgerSprites.Add(child);
        }

        RectTransform[] objectiveDisplays = QuestDisplay.GetComponentsInChildren<RectTransform>();

        foreach (RectTransform child in objectiveDisplays)
        {
            if (child.gameObject.GetComponent<TextMeshProUGUI>() && child.gameObject.tag == "Objective")
            {
                ObjectiveDisplays.Add(child);
            }
        }

        foreach (var quest in currentQuests)
        {
            foreach (var objective in quest.objectives)
            {
                if (objective is AbstractObjective)
                {
                    if (showLoading)
                    {
                        print($"Objective: {objective.name} loaded.");
                    }
                }
                else
                {
                    if (showLoading)
                    {
                        Debug.LogWarning($"Could not load objective: {objective.name}," +
                        $" are you sure this is an objective?");
                    }

                    quest.objectives.Remove(objective);
                }
            }
            foreach (var completeAction in quest.completeActions.ToArray())
            {
                if (completeAction is AbstractCompleteAction)
                {
                    if (showLoading)
                    {
                        print($"Action: {completeAction.name} loaded.");
                    }
                }
                else
                {
                    if (showLoading)
                    {
                        Debug.LogWarning($"Could not load action: {completeAction.name}," +
                        $" are you sure this is an objective?");
                    }

                    quest.completeActions.Remove(completeAction);
                }
            }
        }
    }
    public void SetNextQuest(List<int> nextQuests)
    {
        if (nextQuests.Count > 0)
        {
            foreach (int questIndex in nextQuests)
            {
                if (questIndex <= currentQuests.Count - 1)
                {
                    if (currentQuests[questIndex].isActive == false)
                    {
                        currentQuests[questIndex].isActive = true;
                        if (showLeft)
                        {
                            print($"Enabled quest {currentQuests[questIndex].QuestName}");
                        }
                    }
                }
                else
                {
                    Debug.LogWarning($"{questIndex} is not a valid quest.");
                }
            }
        }
    }

    void Update()
    {
        DisplayQuest();

        foreach (var quest in currentQuests)
        {
            if (quest.isActive)
            {
                AddDisplayQuest(quest);

                objsNotComplete = quest.objectives.Count;
                
                if (showLeft)
                {
                    print($"before: {objsNotComplete}");
                }

                foreach (var objective in quest.objectives)
                {
                    if (objective.isComplete)
                    {
                        objsNotComplete--;
                    }
                    else
                    {
                        objective.UpdateThis();
                    }
                }
                
                if (showLeft)
                {
                    print($"after: {objsNotComplete}");
                }


                if (objsNotComplete <= 0)
                {
                    foreach (var completeAction in quest.completeActions)
                    {
                        completeAction.CompleteAction();
                    }
                    
                    if (showLeft)
                    {
                        print("test complete");
                        print($"Disabled quest {quest.QuestName}");
                    }

                    quest.isActive = false;
                    RemoveDisplayQuest(quest);
                    SetNextQuest(quest.NextQuests);
                }
            }
        }
    }

    void AddDisplayQuest(Quest quest)
    {
        if (displayedQuests.Count > 0)
        {
            bool hasQuest = false;
            foreach (Quest displayQuest in displayedQuests)
            {
                if (displayQuest == quest)
                {
                    hasQuest = true;
                }
            }
            if (!hasQuest)
            {
                displayedQuests.Add(quest);
            }
        }
        else
        {
            displayedQuests.Add(quest);
        }
    }

    void RemoveDisplayQuest(Quest quest)
    {
        displayedQuests.Remove(quest);
    }

    void DisplayQuest()
    {
        //QuestDisplay.GetComponentInChildren<TextMeshProUGUI>().text = displayedQuests[0].QuestName;
        if (displayedQuests.Count > 0)
        {
            QuestDisplay.GetComponentInChildren<TextMeshProUGUI>().text = displayedQuests[0].QuestName;

            if (displayedQuests[0].objectives[0] is ObjectiveObtainBurger)
            {
                DisplayBurger(displayedQuests[0]);
            }
            else
            {
                currentObjectives.Clear();
                foreach (AbstractObjective objective in displayedQuests[0].objectives)
                {
                    if (objective.isComplete)
                    {
                        currentObjectives.Remove(objective);
                    }
                    else
                    {
                        currentObjectives.Add(objective);
                    }
                }

                for (int i = 0; i < ObjectiveDisplays.Count; i++)
                {
                    if (i < currentObjectives.Count)
                    {
                        ObjectiveDisplays[i].GetComponentInChildren<TextMeshProUGUI>().text = currentObjectives[i].ObjectiveName;
                    }
                    else
                    {
                        ObjectiveDisplays[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
                    }
                }
            }
        }
        else
        {
            QuestDisplay.GetComponentInChildren<TextMeshProUGUI>().text = "";

            foreach (RectTransform display in ObjectiveDisplays)
            {
                display.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }

    void DisplayBurger(Quest quest)
    {
        objective = quest.objectives[0].GetComponent<ObjectiveObtainBurger>();

        if (!isBurgerDisplayed)
        {
            foreach (RectTransform image in ActiveBurgerSprites)
            {
                image.gameObject.SetActive(false);
            }

            foreach (RectTransform image in InactiveBurgerSprites)
            {
                image.gameObject.SetActive(true);
            }

            ActiveBurgerSprite.gameObject.SetActive(true);

            isBurgerDisplayed = true;
        }

        if (objective._currentItem)
        {
            for (int i = 0; i < ActiveBurgerSprites.Count; i++)
            {
                if (objective._currentItem.name == ActiveBurgerSprites[i].name)
                {
                    InactiveBurgerSprites[i].gameObject.SetActive(false);
                    ActiveBurgerSprites[i].gameObject.SetActive(true);
                }
            }
        }

        if (objective.partsLeft <= 0)
        {
            isBurgerDisplayed = false;
            ClearBurgerDisplay();
        }
    }

    void ClearBurgerDisplay()
    {
        foreach (RectTransform image in ActiveBurgerSprites)
        {
            image.gameObject.SetActive(false);
        }

        foreach (RectTransform image in InactiveBurgerSprites)
        {
            image.gameObject.SetActive(false);
        }
    }
}
