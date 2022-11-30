using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabObject : MonoBehaviour
{
    public GameObject grabTargetObj;

    public string grabTag = "PhysObject";

    public string ObtainableTag = "Obtainable";

    public float grabDistance;

    public float throwForce = 10f;

    public float orbitForce = 20f;

    public float disconnectDistance = 1f;

    private Vector3 grabTargetLocation;

    [HideInInspector]
    public GameObject grabObject;

    [HideInInspector]
    public GameObject currentItem;


    public void OnPress(InputAction.CallbackContext context)
    {
        if ((context.started || context.performed) && !grabObject)
        {
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


    public void CheckObject(GameObject _object)
    {
        if (_object.tag == grabTag)
        {
            SetGrabObject(_object);
            // Checks if the current object is a obtainable item that would need
            // to be collected
        } else if (_object.tag == ObtainableTag)
        {
            currentItem = _object;

            _object = null;
        }
    }


    public void ClearItem()
    {
        currentItem = null;
    }

    public void SetGrabObject(GameObject _object)
    {
        grabObject = _object;

        grabObject.transform.parent = grabTargetObj.transform;

        grabObject.GetComponent<Rigidbody>().useGravity = false;
    }

    public void ClearGrabObject()
    {
        if (grabObject)
        {
            grabObject.GetComponent<Rigidbody>().freezeRotation = true;
            grabObject.GetComponent<Rigidbody>().freezeRotation = false;


            grabObject.GetComponent<Rigidbody>().useGravity = true;
            grabObject.transform.parent = null;
            grabObject.GetComponent<Rigidbody>().drag = 0;


            grabObject = null;
        }
    }

    public void ConfirmGrabObject()
    {
        RaycastHit hit;

        if (grabObject)
        {
            float distance = Vector3.Distance(PlayerStatic.Player.transform.position, grabObject.transform.position);

            Vector3 dirToPlayer = PlayerStatic.Player.transform.position - grabObject.transform.position;

            //Debug.DrawRay(grabObject.transform.position, dirToPlayer, Color.red);

            if (Physics.Raycast(grabObject.transform.position, dirToPlayer, out hit, distance))
            {
                if (hit.transform.gameObject != PlayerStatic.Player.gameObject &&
                    hit.transform.gameObject != grabObject)
                {
                    ClearGrabObject();
                }
                else if (Vector3.Distance(grabObject.transform.position, grabTargetLocation) > disconnectDistance)
                {
                    ClearGrabObject();
                }

            }
        }
    }


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
    void Update()
    {
        grabTargetLocation = grabTargetObj.transform.position;

        ConfirmGrabObject();
    }

    // Looks for 
    private void Start()
    {
        
    }
}
