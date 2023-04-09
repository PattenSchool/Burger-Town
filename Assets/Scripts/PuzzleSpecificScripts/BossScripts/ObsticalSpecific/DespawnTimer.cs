using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnTimer : MonoBehaviour
{
    #region Data Variables
    [Header("Timer related")]

    [Tooltip("The time before the platform would despawn in seconds")]
    [SerializeField, Min(0.1f)]
    private float despawnTime = 0.1f;
    #endregion

    #region Unity Methods
    private void OnEnable()
    {
        StartCoroutine(StartDespawn());
    }
    #endregion

    #region Coroutine Methods
    private IEnumerator StartDespawn()
    {
        yield return new WaitForSeconds(despawnTime);
        ObjectPooling.Despawn(this.gameObject);
    }
    #endregion
}
