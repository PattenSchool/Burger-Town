using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBolt : BoltTemplate
{
    #region Unity Methods
    private void Update()
    {
        //TODO: Get direction of the player
        this.transform.LookAt(PlayerStatic.Player.transform.position);
        Vector3 directionToPlayer = this.transform.forward;
        
        //TODO: Apply this to the velocity of the rigidbody
        _rigidbody.velocity = directionToPlayer * _initialSpeed;
    }
    #endregion
}
