using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LauncherBolt : BoltTemplate
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
        if (firee.GetComponent<rbCharacterController>().isLaunchedByCannon)
        {
            firee.GetComponent<rbCharacterController>().isLaunchedByCannon = false;

            firee.GetComponent<rbCharacterController>().boltVelocity = directionVector * (_initialSpeed );
        }
        else
        {
            firee.GetComponent<rbCharacterController>().boltVelocity = directionVector * (_initialSpeed / 1.55f);
        }
    }
    #endregion

    protected override void TriggerRaycastCollision(GameObject firee, Vector3 directionVector, RaycastHit hitInfo)
    {
        //TODO: Trigger the object collisions
        TriggerObjectCollision(hitInfo.point, hitInfo.collider, hitInfo.rigidbody);

        //TODO: Trigger the bolt collisions if the object collided was not the player
        if (hitInfo.collider.tag != PlayerStatic.PlayerTag)
            TriggerBoltCollision(hitInfo.point);
    }

    protected override void TriggerObjectCollision
        (Vector3 contactPoint, Collider collider, Rigidbody rigidbody = null)
    {
        //TODO: Get the gameobject
        GameObject collidedGameObject = collider.gameObject;

        //TODO: Trigger any hittable information
        #region Trigger IHitable information
        IHitable hittableInformation = collidedGameObject.GetComponent<IHitable>();
        if (hittableInformation != null)
            hittableInformation.IHit();
        #endregion
    }
}
