using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Strength_Puzzle_Weight : MonoBehaviour
{
    public float speed = 8f;
    public static bool isHit = false;
    public float finalHeight;
    public float heightThreshold = 1f;

    // Start is called before the first frame update
    void Start()
    {
        finalHeight += this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        MoveWeight();
    }


    public void MoveWeight()
    {
        Vector3 moveDirection = Vector3.zero;
        if (isHit == true)
        {

            moveDirection += this.transform.up;
            transform.position += moveDirection.normalized * Time.deltaTime * speed;
            this.GetComponent<Rigidbody>().isKinematic = true;

        }
        else
        {
            moveDirection = Vector3.zero;
        }

        if (Mathf.Abs((finalHeight - this.transform.position.y)) < heightThreshold)
        {
            this.GetComponent<Rigidbody>().isKinematic = false;
            isHit = false;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {

    }


}

