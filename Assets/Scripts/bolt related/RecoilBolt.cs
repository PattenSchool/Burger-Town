using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilBolt : BoltTemplate
{
    #region On Fire Methods
    /// <summary>
    /// Apply effects on the one firing the bolt
    /// </summary>
    /// <param name="firee"></param>
    ///     The one firing the bolt
    public override void OnFire(GameObject firee)
    {
        //Base functions of launched method
        base.OnFire(firee);

        //Apply the recoil to firee
        ApplyKnockbacRecoil(firee);
    }

    /// <summary>
    /// Apply a knockback recoil to a firee in meters per second
    /// </summary>
    /// <param name="firee"></param>
    ///     The one who fired the bolt
    public void ApplyKnockbacRecoil(GameObject firee)
    {
        //Get the rigid body of the one being launched
        Rigidbody rb = firee.GetComponent<Rigidbody>();

        //Set the initial velocity of the player to launch
        rb.velocity = Camera.main.transform.forward * _initialSpeed;
    }
    #endregion
}
