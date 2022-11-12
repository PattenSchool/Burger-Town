using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BoltTemplate : Projectile
{
    #region Components
    [Header("Game Object Components")]

    [Tooltip("The rigid body of this bolt")]
    [SerializeField]
    protected Rigidbody _rigidbody;

    [Tooltip("The collider of the bolt")]
    [SerializeField]
    protected Collider _collider;
    #endregion

    #region Data Variables
    [Header("Data Varaibles")]

    [Tooltip("The location of the local center of mass")]
    [SerializeField]
    protected Vector3 _centerOfMass;

    [Tooltip("The initial speed of the bolt in meters per second")]
    [SerializeField]
    protected float _initialSpeed = 10f;

    [Tooltip("2D image of the bolt")]
    public Sprite sprite;
    #endregion

    #region Time Variables
    [Header("Time Variables")]

    [Tooltip("The desired time bolt to exist in the world in seconds")]
    [SerializeField, Min(0f)]
    protected float _desiredSetTime = 10f;
    #endregion

    #region Unity Methods
    protected void OnEnable()
    {
        //Set up bolt
        if (_rigidbody == null)
            _rigidbody = this.gameObject.GetComponent<Rigidbody>();

        _rigidbody.centerOfMass = _centerOfMass;

        if (_collider == null)
            _collider = this.gameObject.GetComponent<Collider>();
    }

    protected void OnDisable()
    {
        _rigidbody.constraints = RigidbodyConstraints.None;
        _rigidbody.angularVelocity = Vector3.zero;
    }

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

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_centerOfMass, 0.01f);
    }
    #endregion

    #region Collision Methods
    /// <summary>
    /// Used to set the conditions when the bolt hits the object
    /// </summary>
    public override void IHit()
    {
        DespawnFromPool();
    }
    #endregion

    #region On Fire Methods
    /// <summary>
    /// Used to apply an effect to someone who fired the bolt
    /// </summary>
    /// <param name="firee"></param>
    ///     The one who launched the bolt
    public virtual void OnFire(GameObject firee, Vector3 directionVector)
    {
        //Set the speed of the bolt
        _rigidbody.velocity = directionVector * _initialSpeed;

        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX;
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;

        //Set the maximum time the bolt exists
        StartCoroutine(DespawWithTimer());
    }
    #endregion

    #region Coroutine Despawn Methods
    /// <summary>
    /// Despawns this bolt in an object pool by time limit
    /// </summary>
    /// <returns></returns>
    ///     A coroutine result
    protected IEnumerator DespawWithTimer()
    {
        //wait for the bolt to stop
        yield return new WaitForSeconds(_desiredSetTime);

        //Used to despawn the bolt in the object pool
        DespawnFromPool();

        //A safe gaurd
        yield break;
    }

    /// <summary>
    /// Despawns the game obejct with the object pool
    /// </summary>
    protected void DespawnFromPool()
    {
        //Despawn the game object from the object pool
        ObjectPooling.Despawn(this.gameObject);
    }
    #endregion
}
