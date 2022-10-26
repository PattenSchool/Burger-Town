using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A basic bolt template
/// </summary>
public class BoltTemplate : Projectile
{
    #region Game objects
    [Header("Game Objects")]

    [Tooltip("The rigid body of this bolt")]
    [SerializeField]
    protected Rigidbody _rigidbody;

    [Tooltip("The collision of this object")]
    [SerializeField]
    protected Collider _collider;
    #endregion

    #region Collision Variables
    [Header("Collision Variables")]

    [Tooltip("The range in front of the bolt for detection")]
<<<<<<< Updated upstream:Assets/Sage's test stuff/Scripts/bolt related/BoltTemplate.cs
    [SerializeField, Min(0f)]
    private float _detectionRange = 0f;
=======
    [SerializeField, Min(0.01f)]
    private float _detectionRange = 0.1f;

    [Tooltip("The initial speed of the bolt in meters per second")]
    [SerializeField]
    protected float _initialSpeed = 10f;

    [Tooltip("The layermasks that are detected")]
    [SerializeField]
    protected LayerMask _hittableLayers;

    [Tooltip("The radius of the circle")]
    [SerializeField, Min(0.01f)]
    private float _detectionRadius;

    [Tooltip("The origin of the wiresphere")]
    private Vector3 _detectionOrigin;

    [Tooltip("THe direction of the detection wiresphere")]
    private Vector3 _detectionDirection;
>>>>>>> Stashed changes:Assets/Scripts/bolt related/BoltTemplate.cs
    #endregion

    #region Time Variables
    [Header("Time Variables")]

    [Tooltip("The desired time bolt to exist in the world in seconds")]
    [SerializeField, Min(0f)]
    private float _desiredSetTime = 0f;
    #endregion

    #region Data Variables
    [Header("Data Variables")]

    [Tooltip("The bolt layer mask name")]
    private string boltLayerName = "Bolt";
    #endregion

    #region Unity Methods
    private void OnEnable()
    {
        if (_rigidbody == null)
            _rigidbody = this.gameObject.GetComponent<Rigidbody>();
        if (_collider == null)
            _collider = GetComponent<Collider>();

        if (this.gameObject.layer != LayerMask.NameToLayer(boltLayerName))
            this.gameObject.layer = LayerMask.NameToLayer(boltLayerName);
    }

    private void Update()
    {
        //Define how the hit is being set
        RaycastHit hitInfo;

        //Hit physics
        if (IsHitting(out hitInfo))
        {
            //What is being hit
            IHitable hitable = hitInfo.collider.GetComponent<IHitable>();

            Collider hittedCollider = hitInfo.collider;

            //If hitable, then deploy IHitable
            if (hitable != null)
            {
                //Deploy it for object
                hitable.IHit();
            }

            if (hittedCollider != null)
            {
                //Deploy hit for bolt
                IHit();
            }

            
        }
    }

    private void OnDrawGizmos()
    {
        //Calculate the origin of the detection range in the editor
        SetUpDetectionSphere();
        Gizmos.color = Color.red;

        //Beginning of the detection range
        Gizmos.DrawWireSphere(_detectionOrigin, _detectionRadius);
        Gizmos.DrawWireSphere(_detectionOrigin + _detectionDirection * _detectionRange, _detectionRadius);
    }
    #endregion

    #region Collision Methods
    /// <summary>
    /// Used to set the conditions when the bolt hits the object
    /// </summary>
    public override void IHit()
    {
        
    }

    /// <summary>
    /// Checks if the bolt is hitting an object
    /// </summary>
    /// <param name="hitInfo"></param>
    ///     The info of the object being hit, returning any information of it being hit
    /// <returns></returns>
    ///     The result of the cast of the hit
    protected bool IsHitting(out RaycastHit hitInfo)
    {
        _detectionDirection = transform.forward.normalized;
        _detectionOrigin = transform.position + _collider.bounds.extents.z * transform.forward;

        //Check if hitting
        bool hitBool = Physics.SphereCast(_detectionOrigin, _detectionRadius, _detectionDirection,
            out hitInfo, _detectionRange, _hittableLayers);

        //Return the result
        return hitBool;
    }

    /// <summary>
    /// Editor Sphere only
    /// </summary>
    private void SetUpDetectionSphere()
    {
        _detectionOrigin = transform.position + _collider.bounds.extents.z * transform.forward;
        _detectionDirection = transform.forward.normalized;
    }
    #endregion

    #region Launched Methods
    /// <summary>
    /// Used to apply an effect to a launchee or bolt on launch
    /// </summary>
    /// <param name="launchee"></param>
    ///     The one who launched the bolt
    public virtual void OnLaunched(GameObject launchee)
    {
<<<<<<< Updated upstream:Assets/Sage's test stuff/Scripts/bolt related/BoltTemplate.cs
=======
        //Get the direction vector
        Vector3 directionVector = transform.forward;

        //Set the speed of the bolt
        _rigidbody.velocity = directionVector * _initialSpeed;

>>>>>>> Stashed changes:Assets/Scripts/bolt related/BoltTemplate.cs
        //Set the maximum time the bolt exists
        StartCoroutine(Despawn());
    }
    #endregion

    #region Coroutine Methods
    private IEnumerator Despawn()
    {
        //wait for the bolt to stop
        yield return new WaitForSeconds(_desiredSetTime);

        //Used to despawn the bolt in the object pool
        ObjectPooling.Despawn(this.gameObject);

        //A safe gaurd
        yield break;
    }
    #endregion
}
