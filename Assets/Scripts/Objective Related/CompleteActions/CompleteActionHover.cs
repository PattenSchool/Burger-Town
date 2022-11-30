using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteActionHover : AbstractCompleteAction
{
    public GameObject hoverObject;
    public GameObject hoverTarget;

    private Rigidbody hoverObjRigidbody;
    private bool isHover = false;

    public override void CompleteAction()
    {
        if (hoverObject.GetComponent<Rigidbody>())
        {
            hoverObjRigidbody = hoverObject.GetComponent<Rigidbody>();

            isHover = true;

            // Set tag to Untagged so it can no longer be grabbed by player grab script
            hoverObject.tag = "Untagged";
        }
        else
        {
            Debug.LogWarning($"{hoverObject.name} does not have a rigidbody, please add a rigidbody.");
        }
    }

    // Update disables any rigidbody functions of the hoverObject and moves it to hoverTarget
    // put in update since I wasn't able to stop it from rotating or moving when put in a non-update function
    private void Update()
    {
        if (isHover)
        {
            // Set the object position to the target position
            // Disable the object's rigidbody position and gravity
            hoverObject.transform.position = hoverTarget.transform.position;
            hoverObject.transform.rotation = hoverTarget.transform.rotation;

            hoverObjRigidbody.useGravity = false;

            hoverObjRigidbody.constraints = RigidbodyConstraints.FreezeAll
                | RigidbodyConstraints.FreezeRotation;
        }
    }


}
