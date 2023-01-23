using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gate;

    [SerializeField]
    private GameObject[] triggers;

    [SerializeField]
    private float timeToShootTargets = 1f;

    [SerializeField]
    private GameObject playerTeleportObject;

    private float currentTime = 0f;

    private void Update()
    {
        currentTime -= Time.deltaTime;
    }

    public bool IsAnyTriggerActive()
    {
        foreach(var target in triggers)
        {
            if (target.gameObject.activeInHierarchy)
            {
                return true;
            }
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == PlayerStatic.PlayerTag)
        {
            foreach(var trigger in triggers)
            {
                trigger.SetActive(true);
            }
            currentTime = timeToShootTargets;

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == PlayerStatic.PlayerTag)
        {
            if (!IsAnyTriggerActive())
            {
                gate.SetActive(false);
                this.gameObject.SetActive(false);
            }

            if (currentTime <= 0f && IsAnyTriggerActive())
            {
                PlayerStatic.Player.transform.position = 
                    playerTeleportObject.transform.position;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == PlayerStatic.PlayerTag)
        {
            foreach(var trigger in triggers)
            {
                trigger.SetActive(true);
            }
        }
    }
}
