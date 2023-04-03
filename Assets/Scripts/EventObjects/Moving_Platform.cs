using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Moving_Platform : MonoBehaviour
{
    public float speed = 8f;
    private float defaultSpeed;

    public GameObject[] locations;
    private GameObject currentObjective;
    private Vector3 currentDirection;
    private int currentIndex;
    public float radius;

    private GameObject currentStickyPlatform;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = locations[0].transform.position;

        currentObjective = locations[1];

        currentIndex = 1;

        defaultSpeed = speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Function responsible for movement/changing movement.
        //MovementSwitch();

        if (Vector3.Distance(this.transform.position, currentObjective.transform.position) > radius)
        {
            currentDirection = currentObjective.transform.position - this.transform.position;
        }
        else
        {
            if (currentIndex < locations.Length - 1)
            {
                currentIndex++;
                currentObjective = locations[currentIndex];
            }
            else
            {
                currentIndex = 0;
                currentObjective = locations[currentIndex];
            }
        }

        transform.position += currentDirection.normalized * Time.deltaTime * speed;

        if (currentStickyPlatform != null)
        {
            speed = 0f;
            if (!currentStickyPlatform.activeInHierarchy)
            {
                speed = defaultSpeed;
                currentStickyPlatform = null;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == PlayerStatic.PlayerTag)
        other.transform.parent = this.transform;

        if (other.gameObject.layer == 3)
        {
            // Checks if the object colliding is not a sticky bolt or platform
            if (other.tag != "StickyPlatform" || other.tag != "StickyPlatform")
            {
                // Makes the player's transform into the moving platform's transform.
                other.gameObject.transform.parent = this.transform;
            }

            if (other.tag == "StickyPlatform")
            {
                currentStickyPlatform = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.transform.parent = null;
    }
}
