using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strength_Puzzle_Button : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject otherObject = collision.gameObject;
        if (otherObject.tag == "NormalBolt")
        {
            Strength_Puzzle_Weight.isHit = true;
        }
    }

}
