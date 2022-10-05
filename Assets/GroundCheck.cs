using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            return;
        }
        player.GetComponent<rbCharacterController>().SetGrounded(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            return;
        }
        player.GetComponent<rbCharacterController>().SetGrounded(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            return;
        }
        player.GetComponent<rbCharacterController>().SetGrounded(true);
    }
}
