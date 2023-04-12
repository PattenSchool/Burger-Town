using System.Collections;
using System.Collections.Generic;
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

    #region Unity Methods
    private void Update()
    {
        if (spawnTimeRemaining <= 0f)
        {
            //TODO: Spawn new platform 
            Vector3 spawnPos = (Vector3.up * (spawnDistanceFromStart + this.transform.position.y));
            float randomRotation = Random.Range(0f, 360);
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
    }
    #endregion
}
