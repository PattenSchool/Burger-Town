using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBoltMechanics : BoltTemplate
{
    #region Collision Methods
    //The function that makes the sticky bolt function
    public override void IHit()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.useGravity = false;
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }
    #endregion
}
