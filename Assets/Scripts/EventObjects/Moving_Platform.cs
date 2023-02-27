using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Platform : MonoBehaviour
{
    public float speed = 8f;
    private bool reverseDirection = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Function responsible for movement/changing movement.
        MovementSwitch();
    }


    private void OnTriggerEnter(Collider other)
    {
        // Makes the player's transform into the moving platform's transform.
            other.gameObject.transform.parent = this.transform;

        // If the moving platform hits Platform A's collider trigger, change the bool that reverses the moving platform's direction. 
        if (other.gameObject.tag == "PlatformA")
        {
            reverseDirection = true;

            Debug.Log(reverseDirection);
        }

        // If the moving platform hits Platform B's collider trigger, change the bool that reverses the moving platform's direction to its original movement. 
        if (other.gameObject.tag == "PlatformB")
        {
            reverseDirection = false;
        }
    }

    private void OnTriggerExit(Collider other)

    {
        // Makes the player's transform to null so it detaches from the moving platform.
        other.gameObject.transform.parent = null;
    }

    private void MovementSwitch()
    { 
        Vector3 moveDirection = Vector3.zero;

        // Reverse the Moving Platform's direction
        if (reverseDirection == true)
        {
            moveDirection += this.transform.forward;
        }

        // Re-reverse the Moving Platform's direction
        else
        {
            moveDirection -= this.transform.forward;
        }
        
        transform.position += moveDirection.normalized * Time.deltaTime * speed;
    }
}
