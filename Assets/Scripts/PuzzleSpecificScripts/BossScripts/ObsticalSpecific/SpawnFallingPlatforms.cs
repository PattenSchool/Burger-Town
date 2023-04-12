using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField, Min(0f), HideInInspector]
    private float spawnTimeRemaining = 0f;

    [Tooltip("The distance above the player that platofrms will spawn" +
        " in meters")]
    [SerializeField, Min(0f)]
    private float spawnDistanceFromPlayer = 0f;
    #endregion

    #region Unity Methods
    private void Update()
    {
        if (spawnTimeRemaining <= 0f)
        {
            //TODO: Spawn new platform
            Vector3 spawnPos = (Vector3.up * (spawnDistanceFromPlayer + PlayerStatic.Player.transform.position.y));
            float randomRotation = UnityEngine.Random.Range(0f, 360);
            Quaternion rotation = Quaternion.Euler(0f, randomRotation, 0f);

            var platform = ObjectPooling.Spawn(spawnablePlatform, spawnPos, rotation);
            platform.transform.parent = this.gameObject.transform;

            //TODO: Reset timer
            spawnTimeRemaining = spawnDelay;
        }
        else
        {
            spawnTimeRemaining -= Time.deltaTime;
        }
    }
    #endregion
}
