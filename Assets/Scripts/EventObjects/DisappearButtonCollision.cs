using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearButtonCollision : MonoBehaviour
{
    [SerializeField]
    public GameObject stickyPlatform;


    private void OnCollisionEnter(Collision collision)
    {
        // Turns on the boolean responsible for making the blocks appear on when the player is touching the button.
       // DisappearBlockMesh.meshVisible = true;

        GameObject otherObject = collision.gameObject;

        // Turns on the boolean responsible for making the blocks appear on when the player is touching the button.
        if (otherObject.tag == "Player")
        {
            DisappearBlockMesh.meshVisible = true;
        }

        if (otherObject.gameObject.name.Contains(stickyPlatform.name)  == collision.gameObject.name.Contains(stickyPlatform.name))
        {
            DisappearBlockMesh.meshVisible = true;

        }

    }

    private void OnCollisionExit(Collision collision)
    {
       // DisappearBlockMesh.meshVisible = false;
        
        GameObject otherObject = collision.gameObject;

        // Turns off the boolean responsible for making the blocks disappear off when the player is no longer touching the button.
        if (otherObject.tag == "Player")
        {
            DisappearBlockMesh.meshVisible = false;
        }
        /*
        if (otherObject.gameObject.name.Contains(stickyPlatform.name) == collision.gameObject.name.Contains(stickyPlatform.name))
        {
            DisappearBlockMesh.meshVisible = false;
        }
        */
        if (otherObject.activeSelf == false)
        {
            DisappearBlockMesh.meshVisible = false;
        }
    }

}
