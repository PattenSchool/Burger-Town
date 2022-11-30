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
    /// <param name="directionVector"></param>
    ///     The direciton the firee is facing (in vector form)
    public override void OnFire(GameObject firee, Vector3 directionVector)
    {
        //Base functions of launched method
        base.OnFire(firee, directionVector);

        //Apply the recoil to firee
        ApplyKnockbacRecoil(firee, directionVector);
    }

    /// <summary>
    /// Apply a knockback recoil to a firee in meters per second
    /// </summary>
    /// <param name="firee"></param>
    ///     The one who fired the bolt
    /// <param name="directionVector"></param>
    ///     The vector of the firee facing
    public void ApplyKnockbacRecoil(GameObject firee, Vector3 directionVector)
    {
        //Get the rigid body of the one being launched
        Rigidbody rb = firee.GetComponent<Rigidbody>();

        //Set the initial velocity of the player to launch
        rb.velocity = directionVector * _initialSpeed;
    }
    #endregion
}
