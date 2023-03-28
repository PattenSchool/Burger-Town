using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BossBehaviorVariables : MonoBehaviour
{
    [Header("Boss data variables")]

    #region Game Objects
    [Tooltip("The head object")]
    [SerializeField]
    private GameObject head;
    public GameObject Head
    {
        get { return head; }
    }

    [Tooltip("Animation Shell")]
    [SerializeField]
    private BossAnimationPlayer animationShell;
    public BossAnimationPlayer AnimationShell
    {
        get { return animationShell; }
    }

    [Tooltip("The ammo the boss uses")]
    [SerializeField]
    private BoltTemplate ammo;
    public BoltTemplate Ammo
    {
        get { return ammo; }
    }

    [Tooltip("The distractable object")]
    [SerializeField]
    private DistractableObject distractableObject;
    public DistractableObject DistractableObject
    {
        get { return distractableObject; }
    }
    #endregion

    #region Boss Data
    [Tooltip("Time between shots in seconds")]
    [SerializeField, Min(0.5f)]
    private float timeBetweenShots;
    public float TimeBetweenShots
    {
        get { return timeBetweenShots; }
    }

    [Tooltip("The looking direction")]
    private Vector3 lookingDirection;
    public Vector3 LookingDirection
    {
        get { return lookingDirection; }
    }

    [Tooltip("Objects that, if not existing, destroys the enemy")]
    [SerializeField]
    private LifeLinkObject[] lifeLinkObejcts;
    public LifeLinkObject[] LifeLinkObjects
    {
        get { return lifeLinkObejcts; }
    }

    [Tooltip("If enemy is defeated")]
    private bool isDefeated;
    public bool IsDefeated
    {
        get { return isDefeated; }
    }
    #endregion

    #region Unity Methods
    private void OnEnable()
    {
        SetUpDistractbleObject();

        SetUpLifeLinks();
    }

    private void Update()
    {
        //Update variables here
        if (head != null)
        {
            lookingDirection = head.transform.forward;
        }

        //Update defeated state
        FindDefeatedState();
    }
    #endregion

    #region Defeated State Stuff
    /// <summary>
    /// If lifeobject exists, then enemy is not defeated
    /// </summary>
    private void FindDefeatedState()
    {
        //If an object is active, let enemy live
        foreach (var lifeObject in lifeLinkObejcts)
        {
            if (lifeObject.isActiveAndEnabled)
            {
                isDefeated = false;
                return;
            }
        }

        //If no object is active, then enemy is defeated
        isDefeated = true;
    }
    #endregion

    #region Setup Methods
    /// <summary>
    /// Used to set up distractable object for enemy
    /// </summary>
    private void SetUpDistractbleObject()
    {
        //Get every instance of distractable objects
        var distractableObjects = GameObject.FindObjectsOfType<DistractableObject>();

        //If distractable object exists, then ignore rest of thing
        if (distractableObject != null)
        {
            return;
        }

        //Case studies of it
        if (distractableObjects.Length < 1 || distractableObjects == null)
        {
            Debug.LogError("No distractable objects detected");

            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif

        }
        else if (distractableObjects.Length > 1)
        {
            Debug.LogError("Multiple distractable objects detected, grabbing first instance");
            distractableObject = distractableObjects[0];
        }
        else if (distractableObjects.Length == 1)
        {
            distractableObject = distractableObjects[0];
        }
    }

    /// <summary>
    /// Gets life objects to stand in as enemy health
    /// </summary>
    private void SetUpLifeLinks()
    {
        lifeLinkObejcts = FindObjectsOfType<LifeLinkObject>();

        if (LifeLinkObjects.Length < 1)
        {
            Debug.LogError("Please spawn in object with a LifeLinkObject script");
        }
    }
    #endregion
}
