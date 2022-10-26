using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBoltMechanics : BoltTemplate
{
    public override void IHit()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.useGravity = false;
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }
<<<<<<< Updated upstream:Assets/Sage's test stuff/Scripts/bolt related/StickyBoltMechanics.cs
=======

    private void OnDisable()
    {
        _rigidbody.useGravity = true;
        _rigidbody.constraints = RigidbodyConstraints.None;
    }
    #endregion
>>>>>>> Stashed changes:Assets/Scripts/bolt related/StickyBoltMechanics.cs
}
