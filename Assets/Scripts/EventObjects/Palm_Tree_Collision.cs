using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palm_Tree_Collision : MonoBehaviour, IHitable
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

    public void IHit()
    {
        //Plays the idle animation if the Idle Test Object is touched.
        palmTreeAnimation.Play("Palm_Tree_Fall_Down");
        Debug.Log("Tree is falling");
        
    }

}
