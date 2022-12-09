using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palm_Tree_Collision : MonoBehaviour
{

    public Animator palmTreeAnimation;
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
        //Plays the idle animation if the Idle Test Object is touched.
        if (otherObject.tag == "NormalBolt")
        {
            palmTreeAnimation.Play("Palm_Tree_Fall_Down");
            Debug.Log("Tree is falling");
        }
        
    }

}
