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
    [SerializeField, Min(0.1f)]
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
    #endregion

    #region Unity Methods
    private void OnEnable()
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

    private void Update()
    {
        //Update variables here
        if (head != null)
        {
            lookingDirection = head.transform.forward;
        }
    }
    #endregion
}
