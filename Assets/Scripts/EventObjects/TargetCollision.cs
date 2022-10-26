using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collider)
    {
        //Destroys the Target Object
        Destroy(this.gameObject);
    }

    public void OnDestroy()
    {
        // Lowers the targetNum counter in the GateTrigger script by 1.
        GateTrigger.targetNum-= 1;
    }
}
