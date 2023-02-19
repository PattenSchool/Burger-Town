using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strength_Puzzle_Weight : MonoBehaviour
{
    public float speed = 0.01f;
    public static bool isHit = false;
    // Start is called before the first frame update
    void Start()
    {
        
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
        }
        else
        {
            moveDirection = Vector3.zero;
        }
       isHit = false;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }


}

