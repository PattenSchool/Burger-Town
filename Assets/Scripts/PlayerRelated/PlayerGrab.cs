using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGrab : MonoBehaviour
{
    public GameObject grabberCollider;
    public float grabDistance;


    [HideInInspector]
    public GameObject grabObject = null;


    private GameObject _player;
    private GameObject _cameraObject;
    private GameObject updatePlayer;

    private Vector3 throwDirection;

    public float throwForce = 25;

    private bool isThrown = false;

    public string currentTag = null;

    private bool isGrab;

    public void Start()
    {
        _cameraObject = Camera.main.gameObject;
        _player = GameObject.FindGameObjectWithTag("Player");

        //throwForce = 50f;
    }

    public void FixedUpdate()
    {
        //throwDirection = (_cameraObject.transform.position + test.transform.position);

        if (grabObject)
        {
            grabObject.transform.rotation = Quaternion.Euler( 0f,
                _cameraObject.transform.eulerAngles.y, 0f);



            if (Vector3.Distance(grabObject.transform.position, grabberCollider.transform.position) > 0.05f)
            {
                //print(grabObject.GetComponent<Rigidbody>().useGravity);
                Vector3 moveDirection = (grabberCollider.transform.position - grabObject.transform.position);
                //grabObject.GetComponent<Rigidbody>().AddForce(moveDirection * /*pickupForce*/ 20);
                grabObject.GetComponent<Rigidbody>().velocity = (moveDirection * /*pickupForce*/ 20);
                grabObject.GetComponent<Rigidbody>().drag = 0f;
            }
            else if (Vector3.Distance(grabObject.transform.position, grabberCollider.transform.position) < 0.05f)
            {
                grabObject.GetComponent<Rigidbody>().drag = 10f;
            }
        }
    }

    public void Update()
    {
        CheckForObject();
    }

    public void OnGrab(InputAction.CallbackContext context)
    {

        if ((context.started || context.performed ) && !isThrown)
        {
            if (currentTag == "PhysObject")
            {
                Grab();
            }
        }
        if (context.canceled || currentTag != "PhysObject")
        {
            
            if (grabObject)
            {
                CancelGrab();
                ClearGrabObject();
            }
        }
    }

    void Grab()
    {
        isGrab = true;

        if (grabObject)
        {
            grabObject.transform.parent = grabberCollider.transform;
            
            //grabObject.GetComponent<Rigidbody>().freezeRotation = true;
            grabObject.GetComponent<Rigidbody>().useGravity = false;
            //grabObject.GetComponent<Rigidbody>().drag = 7;
        }
    }

    void CancelGrab()
    {
        isGrab = false;

        //grabObject.transform.parent = null;
        //grabObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        // Reset rotation
        grabObject.GetComponent<Rigidbody>().freezeRotation = true;
        grabObject.GetComponent<Rigidbody>().freezeRotation = false;

        
        grabObject.GetComponent<Rigidbody>().useGravity = true;
        grabObject.transform.parent = null;
        grabObject.GetComponent<Rigidbody>().drag = 0;
        //grabObject.GetComponent<Rigidbody>().freezeRotation = false;
        //grabObject = null;
    }

    void ClearGrabObject()
    {
        if (grabObject)
        {
            grabObject = null;
        }
    }

    void CheckForObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(_cameraObject.transform.position, _cameraObject.transform.TransformDirection(Vector3.forward), out hit, grabDistance))
        {
            currentTag = hit.transform.gameObject.tag;
            if (isGrab && currentTag == "PhysObject")
            {
                grabObject = hit.transform.gameObject;
            }
            else if (currentTag != "PhysObject")
            {
                if (grabObject)
                {
                    CancelGrab();
                    ClearGrabObject();
                }
            }
        }
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (grabObject)
        {
            isThrown = true;

            CancelGrab();

            //ThrowObject();
            grabObject.GetComponent<Rigidbody>().velocity = (_cameraObject.transform.forward * throwForce);

            ClearGrabObject();
            //ClearGrabObject();

            isThrown = false;
        }
        //Vector3 throwDirection = (_player.transform.position + test.transform.position);
        //Vector3 throwDirection = (updatePlayer.transform.position + test.transform.position);
        //grabObject.GetComponent<Rigidbody>().drag = 0;
        //grabObject.GetComponent<Rigidbody>().AddForce(transform.forward * 200f);

        //throwDirection = (_cameraObject.transform.position - )


        //grabObject.transform.parent = null;
        //grabObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        //grabObject.GetComponent<Rigidbody>().freezeRotation = false;
        //grabObject.GetComponent<Rigidbody>().useGravity = true;
        //grabObject.GetComponent<Rigidbody>().drag = 0;
        //grabObject = null;
    }

    void ThrowObject()
    {
        grabObject.GetComponent<Rigidbody>().AddForce(_cameraObject.transform.eulerAngles * throwForce);
    }
}
