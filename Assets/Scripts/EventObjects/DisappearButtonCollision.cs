using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearButtonCollision : MonoBehaviour
{
    // Temporary gameobject that is set when either the player
    // or sticky bolt touch the button
    private GameObject objectToCheck;

    // Child gameobject of the button that is used for checking the distance
    // of the player and the button
    public GameObject distanceCheck;

    // Max distance allowed between the button and player that can be set
    // in the inspector
    public float radius;

    // Switched to on trigger enter due to several issues detecting platform
    // collision was detecting the bolt rather than the spawned platform
    private void OnTriggerStay(Collider collider)
    {
        // Checks the tag of the colliding object
        // if its a player
        if (collider.transform.gameObject.tag == PlayerStatic.PlayerTag)
        {
            // This is a preventitive measure so that the player can't
            // shutoff the button while a platform is still active on the button
            if (objectToCheck)
            {
                if (objectToCheck.tag != "StickyPlatform")
                {
                    DisappearBlockMesh.meshVisible = true;

                    // Sets the object to check as the player
                    objectToCheck = collider.gameObject;
                }
            }
            // Sets the objectToCheck if there is nothing else currently colliding
            else
            {
                DisappearBlockMesh.meshVisible = true;

                // Sets the object to check as the player
                objectToCheck = collider.gameObject;
            }
        }

        // Checks if collider is sticky platform
        if (collider.transform.gameObject.tag == "StickyPlatform")
        {
            DisappearBlockMesh.meshVisible = true;

            // Sets the object to check as the sticky platform
            objectToCheck = collider.gameObject;
        }
    }

    // Using fixed update due to preference
    // This checks if the player or platform are still colliding and shut off the platforms if so
    private void FixedUpdate()
    {
        // If there is an object touching
        if (objectToCheck != null)
        {
            if (objectToCheck.tag == PlayerStatic.PlayerTag)
            {
                // Distance check between the player and button
                if (Vector3.Distance(PlayerStatic.Player.transform.position, distanceCheck.transform.position) > radius)
                {
                    DisappearBlockMesh.meshVisible = false;

                    // Resets the objectToCheck
                    objectToCheck = null;
                }
            }
            else
            {
                // Checks if the object is currently active
                // couldn't use OnTriggerExit since the sticky platform never exits but 
                // rather is disabled
                if (!objectToCheck.activeInHierarchy)
                {
                    // Sets the objectToCheck to the player if the player is colliding when a sticky platform despawns
                    if (Vector3.Distance(PlayerStatic.Player.transform.position, distanceCheck.transform.position) > radius)
                    {
                        // Disables platforms if sticky platforms are no longer colliding as well as the player
                        DisappearBlockMesh.meshVisible = false;
                        objectToCheck = null;
                    }
                    else
                    {
                        objectToCheck = PlayerStatic.Player;
                    }
                }
            }
        }
    }
}
