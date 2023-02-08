using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabObject : MonoBehaviour
{
    // Location that the grab object moves to
    // Based on MainCamera's location
    public GameObject grabTargetObj;
    private Vector3 grabTargetLocation;

    // Tags of a grabbed object
    // PhysObject to apply physics and grab object
    public string grabTag = "PhysObject";
    // Obtainable to "obtain" the item into an inventory
    public string ObtainableTag = "Obtainable";

    // Amount of force to apply when manually thrown
    // (without shooting crossbow)
    public float throwForce = 10f;

    // Amount of force applied to a grabbed object
    // in order to hover around grabTargetObj
    public float orbitForce = 20f;

    // Distance of grab
    public float grabDistance;

    // Distance between a grabbed object and the player
    // Any further distance would cancel the grab
    public float disconnectDistance = 1f;

    [HideInInspector]
    public GameObject grabObject;

    // Used with the obtainable tag to obtain items
    [HideInInspector]
    public GameObject currentItem;

    // Material used to apply transparency
    public Material transparencyMat;

    // Original material of a grabbed object
    private Material defaultMat;

    // Main camera since static doesn't work
    private GameObject _mainCamera;

    public void Start()
    {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    public void OnPress(InputAction.CallbackContext context)
    {
        // If the grab button is pressed and there is no currently grabbed object
        if ((context.started || context.performed) && !grabObject)
        {
            // Sends out a raycast to check the object the player is looking at
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, grabDistance))
            {
                CheckObject(hit.transform.gameObject);
            }
        }else if(context.started && grabObject)
        {
            ClearGrabObject();
        }
    }

    // Throws (and clears) the grab obejct if the throw button is pressed
    public void OnThrow(InputAction.CallbackContext context)
    {
        if (grabObject)
        {
            GameObject tempObject = grabObject;

            ClearGrabObject();

            tempObject.GetComponent<Rigidbody>().velocity = (Camera.main.transform.forward * throwForce);

            tempObject = null;
        }
    }

    // Checks a given gameobject if it can be grabbed
    public void CheckObject(GameObject _object)
    {
        if (_object.tag == grabTag && CastCheck(_object))
        {
            SetGrabObject(_object);
        // Checks if the current object is a obtainable item that would need
        // to be collected
        } else if (_object.tag == ObtainableTag)
        {
            // Sets current item (intent is to check this script's currentItem in outside scripts)
            currentItem = _object;

            // (TEMP) clear grabObject
            _object = null;
        }
    }

    // Clears the item if it was set
    // NOT GRAB OBJECT
    public void ClearItem()
    {
        currentItem = null;
    }

    // Sets a given gameobject to a grabbed object
    public void SetGrabObject(GameObject _object)
    {
        grabObject = _object;

        // Saves the current material of the grabbed object
        defaultMat = grabObject.GetComponent<MeshRenderer>().material;

        // Changes the grabbed object's material to the transparent material
        grabObject.GetComponent<MeshRenderer>().material = transparencyMat;

        // Sets grab object's parent to the gameobject used to as the location
        grabObject.transform.parent = grabTargetObj.transform;

        // Disables the grab object's gravity
        grabObject.GetComponent<Rigidbody>().useGravity = false;
    }

    // Clears the current grab object
    public void ClearGrabObject()
    {
        // If there is a currently grabbed object
        if (grabObject)
        {
            // Resets all applied properties to a grabbed object when it is being grabbed
            grabObject.GetComponent<Rigidbody>().freezeRotation = true;
            grabObject.GetComponent<Rigidbody>().freezeRotation = false;


            grabObject.GetComponent<Rigidbody>().useGravity = true;

            // Resets the grab object's parent
            grabObject.transform.parent = null;
            grabObject.GetComponent<Rigidbody>().drag = 0;

            // Resets the material of the grabbed object
            grabObject.GetComponent<MeshRenderer>().material = defaultMat;

            // Clears the default material of the grabbed object
            defaultMat = null;

            // Clears grab object
            grabObject = null;
        }
    }

    // Called in update
    // Checks if the grab object should still be grabbed
    public void ConfirmGrabObject()
    {
        RaycastHit hit;

        if (grabObject)
        {
            // Gets distance between grab object and player
            float distance = Vector3.Distance(PlayerStatic.Player.transform.position, grabObject.transform.position);

            // Generates a vector direction (FROM THE GRAB OBJECT) to the player
            Vector3 dirToPlayer = PlayerStatic.Player.transform.position - grabObject.transform.position;
           
            if (Physics.Raycast(grabObject.transform.position, dirToPlayer, out hit, distance))
            {
                // Checks if the grab object is currently visible to the player
                // (Not behind wall)
                if (hit.transform.gameObject != PlayerStatic.Player.gameObject &&
                    hit.transform.gameObject != grabObject)
                {
                    ClearGrabObject();
                }
                // Checks if the grab object is too far away from the player
                else if (Vector3.Distance(grabObject.transform.position, grabTargetLocation) > disconnectDistance)
                {
                    ClearGrabObject();
                }

            }

            // Box cast to check if the player is on top of the grab object
            if (!CastCheck(grabObject))
            {
                ClearGrabObject();
            }
        }
    }

    public bool CastCheck(GameObject _object)
    {
        Vector3 center = _object.transform.position;
        Vector3 halfExtents = _object.gameObject.transform.lossyScale * (0.5f);
        Vector3 direction = Vector3.up;
        Quaternion rotation = Quaternion.identity;
        float rayDistance = 5f;

        RaycastHit raycastHit;

        if (Physics.BoxCast(center, halfExtents, direction, out raycastHit, rotation, rayDistance))
        {
            if (raycastHit.transform.gameObject.tag == PlayerStatic.PlayerTag)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return true;
        }
    }

    // Moves the grab object to the appropriate location
    void FixedUpdate()
    {
        if (grabObject)
        {
            grabObject.transform.rotation = Quaternion.Euler(0f,
                Camera.main.transform.eulerAngles.y, 0f);

            if (Vector3.Distance(grabObject.transform.position, grabTargetLocation) > 0.05f)
            {
                Vector3 moveDirection = (grabTargetLocation - grabObject.transform.position);
                grabObject.GetComponent<Rigidbody>().velocity = (moveDirection * orbitForce);
                grabObject.GetComponent<Rigidbody>().drag = 0f;
            }
            else if (Vector3.Distance(grabObject.transform.position, grabTargetLocation) < 0.25f)
            {
                grabObject.GetComponent<Rigidbody>().drag = 10f;
            }
        }
    }

    // Updates the location for where the grab object should travel to
    // Confirms if the grab object should still be grabbed
    void Update()
    {
        grabTargetLocation = grabTargetObj.transform.position;

        ConfirmGrabObject();
    }
}
