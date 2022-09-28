using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltTemplate : Projectile
{
    #region Game objects
    [Header("Game Objects")]

    [Tooltip("The rigid body of this bolt")]
    [SerializeField]
    private Rigidbody _rigidbody;
    #endregion

    #region Data Variables
    [Header("Collision Variables")]

    [Tooltip("The range in front of the bolt for detection")]
    [SerializeField, Min(0f)]
    private float _detectionRange = 0f;
    #endregion

    #region Unity Methods
    private void OnEnable()
    {
        if (_rigidbody == null)
            _rigidbody = this.gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Cast a raycast in front
        bool hit = Physics.Raycast(this.gameObject.transform.position,
            Vector3.forward, _detectionRange);

        if (hit)
        {
            IHit();
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(this.gameObject.transform.position, Vector3.forward * _detectionRange);
    }
    #endregion

    #region Collision Methods
    /// <summary>
    /// Used to set the conditions when the bolt hits the object
    /// </summary>
    public override void IHit()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.useGravity = false;
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
    }
    #endregion

    #region Launched Methods
    /// <summary>
    /// Used to apply an effect to a launchee
    /// </summary>
    /// <param name="launchee"></param>
    ///     The one who launched the bolt
    public virtual void OnLaunched(GameObject launchee)
    {
        //Override information here
        launchee.GetComponent<Rigidbody>().AddForce(_rigidbody.velocity * -1f, ForceMode.Force);
    }
    #endregion
}
