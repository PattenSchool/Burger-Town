using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearButtonCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Turns on the boolean responsible for making the blocks appear on when the player is touching the button.

        DisappearBlockMesh.meshVisible = true;

        
        GameObject otherObject = collision.gameObject;

        Debug.Log(otherObject.tag);
        /*
        if (otherObject.tag == "Player" || otherObject.tag == "StickyPlatform")
        {
            DisappearBlockMesh.meshVisible = true;
        }
        */
        
    }

    private void OnCollisionExit(Collision collision)
    {
        DisappearBlockMesh.meshVisible = false;
        
        // Turns off the boolean responsible for making the blocks disappear off when the player is no longer touching the button.
        GameObject otherObject = collision.gameObject;
        /*
        if (otherObject.tag == "Player" || otherObject.tag == "StickyPlatform")
        {
            DisappearBlockMesh.meshVisible = false;
        }

        */
    }
}
