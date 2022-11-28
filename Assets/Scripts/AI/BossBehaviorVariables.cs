using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private DistractableObject[] distractableObjects;
    public DistractableObject[] DistractableObjects
    {
        get { return distractableObjects; }
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
        distractableObjects = GameObject.FindObjectsOfType<DistractableObject>();
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
