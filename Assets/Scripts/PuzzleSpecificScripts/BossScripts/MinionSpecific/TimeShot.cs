using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeShot : MonoBehaviour
{
    #region Bolt Related
    [Header("Bolt related")]

    [Tooltip("The bolt being fired ")]
    [SerializeField]
    private BoltTemplate ammo;

    [Tooltip("How far the bolt is when spawned in meters")]
    [SerializeField]
    private float boltSpawnSpace = 1f;

    private void FireBolt()
    {
        //TODO: Safegaurd against no ammo
        if (ammo == null) return;

        //TODO: Fire ammo
        Vector3 directionFacing = this.transform.forward;

        //Fire at the direction being faced
        var ammoTemplate = ammo;
        var spawnedAmmo =
            ObjectPooling.Spawn(ammoTemplate.gameObject,
            (directionFacing * boltSpawnSpace) + this.transform.position,
            this.transform.rotation);

        spawnedAmmo.GetComponent<BoltTemplate>().OnFire(this.gameObject, directionFacing);
    }
    #endregion

    #region Timer Related
    [Header("Timer related")]

    [Tooltip("The cooldown of the timer between shots in seconds")]
    [SerializeField]
    private float cooldown;

    [Tooltip("The remaining time of the timer")]
    [SerializeField, HideInInspector]
    private float timer = 0f;
    #endregion

    #region Unity Methods
    private void OnEnable()
    {
        timer = cooldown;
    }

    private void Update()
    {
        //Update the timer
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            //TODO: Fire the bolt
            FireBolt();

            //TODO: Reset the timer
            timer = cooldown;
        }
    }
    #endregion
}
