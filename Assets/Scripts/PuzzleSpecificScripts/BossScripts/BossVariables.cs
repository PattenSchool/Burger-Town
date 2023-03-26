using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossVariables : MonoBehaviour
{
    [Header("Boss data variables")]

    #region Game Objects
    [Tooltip("The object being used to determine which direction is being fired at")]
    [SerializeField]
    private GameObject head;
    public GameObject Head
    {
        get { return head; }
    }

    #endregion

    #region Bolt Related
    [Header("Bolt related")]

    [Tooltip("Possible bolts being fired")]
    [SerializeField]
    private BoltTemplate[] possibleBolts;
    public BoltTemplate[] PossibleBolts
    {
        get { return possibleBolts; }
    }

    [Tooltip("The ammount of time between shots")]
    [SerializeField, Min(0.1f)]
    private float cooldown = 1f;
    public float Cooldown
    {
        get { return cooldown; }
    }
    #endregion


}
