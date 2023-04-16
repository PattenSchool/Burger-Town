using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class SpawnFallingPlatforms : MonoBehaviour
{
    #region GameObject reference
    [Header("GameObjects")]

    [Tooltip("The platform to be spawned in")]
    [SerializeField]
    private GameObject spawnablePlatform;
    #endregion

    #region Data Related
    [Header("Data Variables")]

    [Tooltip("The ammount of time in seconds between platforms")]
    [SerializeField, Min(0.1f)]
    private float spawnDelay = 1.0f;

    [Tooltip("The time remaining until next spawn")]
    [SerializeField, Min(0f)]
    private float spawnTimeRemaining = 0f;

    [Tooltip("The distance above the player that platofrms will spawn" +
        " in meters")]
    [SerializeField, Min(0f)]
    private float spawnDistanceFromStart = 0f;
    #endregion

    #region Rotation related
    [Header("Rotation Generation")]

    [Tooltip("The ammount of sections devided of the circle, " +
        "where possible spawns are. This = 360 degrees")]
    [SerializeField, Min(1)]
    private int maxSections = 6;

    [Tooltip("The offset of each generation of a platform")]
    [SerializeField, Range(-360f, 360)]
    private float degreeOffset = 0f;

    /// <summary>
    /// Generates the rotation of the next platform in degrees
    /// </summary>
    /// <returns></returns>
    private float CalculateRotation(int spawnedSection)
    {
        //TODO: Start the test result, even in case of a null
        float result = 0f;

        #region Generate Actual rotation
        //TODO: Get the entire circle in degrees
        float fullCircleAngle = 360f;

        //TODO: Set the result to the spawned section
        result = fullCircleAngle / (spawnedSection);

        //TODO: Apply the offset to the result
        result += degreeOffset;

        #endregion
        //TODO: Return the result
        return result;
    }

    /// <summary>
    /// Returns a random section between [1, maxSections]
    ///     between 1 and maxSections inclusive to both
    /// </summary>
    /// <returns></returns>
    private int GenerateSection()
    {
        int result = 0;

        //TODO: Generate the random section
        result = Random.Range(1, maxSections + 1);

        return result;
    }
    #endregion

    #region Unity Methods
    private void Update()
    {
        if (spawnTimeRemaining <= 0f)
        {
            //TODO: Spawn new platform 
            Vector3 spawnPos = (Vector3.up * (spawnDistanceFromStart + this.transform.position.y));
            float randomRotation = CalculateRotation(GenerateSection());
            Quaternion rotation = Quaternion.Euler(0f, randomRotation, 0f);

            var platform = ObjectPooling.Spawn(spawnablePlatform, spawnPos, rotation);
            platform.transform.parent = this.gameObject.transform;
            platform.transform.position = spawnPos;
            //TODO: Reset timer
            spawnTimeRemaining = spawnDelay;
        }
        else
        {
            spawnTimeRemaining -= Time.deltaTime;
        }

    }
    #endregion

    #region Debug Options
    private void OnDrawGizmos()
    {
        #region Generate Spawn height Cubes
        Vector3 spawnCenter = new Vector3(
            this.transform.position.x,
            this.transform.position.y + spawnDistanceFromStart,
            this.transform.position.z);
        Vector3 currentPos = this.transform.position;
        float lineLength = 4f;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(spawnCenter, spawnCenter + (Vector3.left * lineLength));
        Gizmos.DrawLine(spawnCenter, spawnCenter + (Vector3.right * lineLength));
        Gizmos.DrawLine(spawnCenter, spawnCenter + (Vector3.forward * lineLength));
        Gizmos.DrawLine(spawnCenter, spawnCenter + (Vector3.back * lineLength));
        Gizmos.DrawLine(spawnCenter, currentPos);
        Gizmos.DrawLine(currentPos, currentPos + (Vector3.left * lineLength));
        Gizmos.DrawLine(currentPos, currentPos + (Vector3.right * lineLength));
        Gizmos.DrawLine(currentPos, currentPos + (Vector3.forward * lineLength));
        Gizmos.DrawLine(currentPos, currentPos + (Vector3.back * lineLength));
        Gizmos.DrawLine(currentPos, currentPos);
        Gizmos.DrawLine(spawnCenter + (Vector3.left * lineLength), currentPos + (Vector3.left * lineLength));
        Gizmos.DrawLine(spawnCenter + (Vector3.right * lineLength), currentPos + (Vector3.right * lineLength));
        Gizmos.DrawLine(spawnCenter + (Vector3.forward * lineLength), currentPos + (Vector3.forward * lineLength));
        Gizmos.DrawLine(spawnCenter + (Vector3.back * lineLength), currentPos + (Vector3.back * lineLength));
        #endregion

        #region Generate possible sections
        //TODO: Give the gizmos a color
        Gizmos.color = Color.blue;

        //TODO: Define direction
        float directionRadius = 1f;

        //TODO: Draw a line showing possible sections
        for(int i = 0; i < maxSections; i++)
        {
            //TODO: Generate vector of direction
            float directionAngle = CalculateRotation(i + 1);



            ////TODO: Calculate the vectors needed
            //Vector3 center = this.transform.position;
            //Vector3 spawnHeightPosition = this.transform.position + new Vector3(0f, spawnDistanceFromStart);
            //Vector3 directionOnFloor = Mathf.Cos(directionAngle * Mathf.Deg2Rad);

            ////TODO: Draw line from center to direction
            //Gizmos.DrawLine();

            //TODO: Draw line from spawn height to direction

            //TODO: Draw line from spawn height direction to center direction

        }
        #endregion
    }
    #endregion
}