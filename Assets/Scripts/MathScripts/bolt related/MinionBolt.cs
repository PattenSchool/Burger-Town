using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionBolt : BoltTemplate
{
    #region GameObject Spawn
    [Header("Minion related")]

    [Tooltip("The minion that is being spawned")]
    [SerializeField]
    private GameObject Minion;
    #endregion

    #region Contact point info
    [SerializeField]
    private Collision collisionInfo;
    #endregion

    new public void OnCollisionEnter(Collision collision)
    {
        collisionInfo = collision;

        base.OnCollisionEnter(collision);
    }

    override public void IHit()
    {
        if (Minion != null)
        {
            ObjectPooling.Spawn(Minion, collisionInfo.contacts[0].point, this.transform.rotation);
        }

        base.IHit();
    }
}
