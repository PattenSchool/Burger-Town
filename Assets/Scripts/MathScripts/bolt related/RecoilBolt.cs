using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
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
        firee.GetComponent<rbCharacterController>().boltVelocity = directionVector * _initialSpeed;
    }
    #endregion

    /// <summary>
    /// Used to apply the hitted effect when the bolt hits something
    /// </summary>
    /// <param name="collision"></param>
    ///     The info of the game object being collided
    protected void OnCollisionEnter(Collision collision)
    {
        //Test to see if there is a hitable interface on the other collider
        IHitable hittableObejct = collision.gameObject.GetComponent<IHitable>();



        //Activate object IHitable
        if (hittableObejct != null && collision.gameObject.tag != PlayerStatic.PlayerTag)
        {
            hittableObejct.IHit();
        }

        //Activate bolt Ihitable
        IHit();
    }
}
