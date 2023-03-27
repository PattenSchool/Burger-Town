using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnWithTimer : MonoBehaviour
{
    #region Timer Variables
    [Header("Timer Related")]

    [Tooltip("The cooldown in seconds that the minion exists")]
    [SerializeField, Min(0.1f)]
    private float cooldown = 1;

    [Tooltip("The timer in seconds displaying the time remaining")]
    [SerializeField, Min(0.1f), HideInInspector]
    private float timer = 1f;
    #endregion

    #region Unity Methods
    private void OnEnable()
    {
        timer = cooldown;
    }

    private void Update()
    {
        if (timer <= 0f)
        {
            //TODO: Despawn object from object pool if existed
            ObjectPooling.Despawn(this.gameObject);
        }

        timer -= Time.deltaTime;
    }
    #endregion
}
