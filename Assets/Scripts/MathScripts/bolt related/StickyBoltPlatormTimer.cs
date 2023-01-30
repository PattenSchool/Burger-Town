using System.Collections;
using UnityEngine;

public class StickyBoltPlatormTimer : MonoBehaviour
{
    #region timer Variables
    [Header("Timer")]

    [Tooltip("Timer delaying despawn")]
    [SerializeField, Min(0.5f)]
    private float despawnInSeconds = 1f;
    #endregion

    #region Unity Methods
    private void OnEnable()
    {
        StartCoroutine(DespawnWithTimer(despawnInSeconds));
    }

    #endregion

    #region Despawn Methods
    /// <summary>
    /// Despawns the game obejct with the object pool
    /// </summary>
    private void DespawnFromPool()
    {
        //Despawn the game object from the object pool
        ObjectPooling.Despawn(this.gameObject);
    }

    /// <summary>
    /// Despawns this bolt in an object pool by time limit
    /// </summary>
    /// <returns></returns>
    ///     A coroutine result
    public IEnumerator DespawnWithTimer(float timerSet)
    {
        //wait for the bolt to stop
        yield return new WaitForSeconds(timerSet);

        //Used to despawn the bolt in the object pool
        DespawnFromPool();

        //Double check despawn
        this.gameObject.SetActive(false);

        //A safe gaurd
        yield break;
    }
    #endregion
}
