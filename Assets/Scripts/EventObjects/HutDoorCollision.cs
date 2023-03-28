using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HutDoorCollision : MonoBehaviour, IHitable
{
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
        //Destroys the Hut Gate Object
        this.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //GameObject otherObject = collision.gameObject;
        ////Plays the idle animation if the Idle Test Object is touched.
        //if (otherObject.tag == "NormalBolt")
        //{
        //    this.gameObject.SetActive(false);
        //}

    }


}
