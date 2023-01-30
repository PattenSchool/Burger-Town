using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    [Tooltip("Takes the position and teleport player there")]
    [SerializeField]
    private GameObject teleportObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == PlayerStatic.PlayerTag)
        {
            PlayerStatic.Player.transform.position = teleportObject.transform.position;
        }
    }
}
