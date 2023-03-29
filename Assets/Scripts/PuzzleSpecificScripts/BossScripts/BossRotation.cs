using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRotation : MonoBehaviour
{
    public float speed;

    #region Varaibles
    [Header("Transform reference")]

    [Tooltip("The transformation that is being referenced")]
    [SerializeField]
    private Transform transformRef;


    [Header("Data variables")]

    [Tooltip("The time it would take to get the rotation")]
    [SerializeField]
    private float timeCount = 0.0f;
    #endregion

    #region UnityMethods
    private void Update()
    {
        //TODO: Get original rotation 
        Quaternion targetRot = transformRef.rotation;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, Time.deltaTime * speed);
        timeCount += Time.deltaTime;
    }
    #endregion
}
