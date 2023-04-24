using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting; 

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

    [Tooltip("If the bolt spawns as a gameobject or a raycast")]
    [SerializeField]
    protected bool isSpawnable = true;
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
        //TODO: Trigger the object collision
        TriggerObjectCollision(collision);

        //TODO: Trigger the bolt collision
        TriggerBoltCollision(collision);
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
        //TODO: Set bolt information if isSpawnable is true
        if (isSpawnable)
        {
            OnSpawn(firee, directionVector);
        }
        //TODO: Set bolt as a line for instant travel
        else
        {
            OnLineDrawn(firee, directionVector);
        }
    }

    /// <summary>
    /// Methods that are done when spawned
    /// </summary>
    /// <param name="firee"></param>
    /// <param name="directionVector"></param>
    protected virtual void OnSpawn(GameObject firee, Vector3 directionVector)
    {
        //Set the speed and constraints of the bolt
        _rigidbody.velocity = directionVector * _initialSpeed;

        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX;
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;

        _rigidbody.useGravity = false;

        //Set the maximum time the bolt exists
        StartCoroutine(DespawWithTimer());
    }

    /// <summary>
    /// The method called if the bolt is raycasted
    /// </summary>
    /// <param name="firee"></param>
    /// <param name="directionVector"></param>
    protected virtual void OnLineDrawn(GameObject firee, Vector3 directionVector)
    {
        //TODO: Get the collision of the raycast
        RaycastHit raycastCollision = GetCollisionInfo(firee, directionVector);

        //TODO: Trigger the collision
        TriggerRaycastCollision(firee, directionVector, raycastCollision);

        //TODO: Apply bolt physics to the collider
        if (raycastCollision.rigidbody != null)
        {
            raycastCollision.rigidbody.AddForceAtPosition(directionVector * _initialSpeed, raycastCollision.point);
        }

        //TODO: Trigger the despawn
        DespawnFromPool();
    }
    #endregion

    #region Raycast Methods
    /// <summary>
    /// The collision info of raycasts
    /// </summary>
    /// <param name="firee"></param>
    ///     The firee that fired the bolt
    /// <param name="directionVector"></param>
    ///     The direction the bolt has been fired
    /// <returns></returns>
    protected virtual RaycastHit GetCollisionInfo(GameObject firee, Vector3 directionVector)
    {
        //TODO: Generate the information with the hit info
        Ray rayDirection = new Ray(this.transform.position, directionVector.normalized);
        RaycastHit hit;
        Physics.Raycast(rayDirection, out hit);

        return hit;
    }


    protected virtual void TriggerRaycastCollision(GameObject firee, Vector3 directionVector, RaycastHit hitInfo)
    {
        //TODO: Trigger the object information
        TriggerObjectCollision(hitInfo.point, hitInfo.collider, hitInfo.rigidbody);

        //TODO: Trigger the bolt collision
        TriggerBoltCollision(hitInfo.point);
    }


    /// <summary>
    /// Trigger the object collision from the bolt (just in case)
    /// </summary>
    /// <param name="collision"></param>
    ///     The collision information
    protected virtual void TriggerObjectCollision(Collision collision)
    {
        TriggerObjectCollision(collision.contacts[0].point, collision.collider, collision.rigidbody);
    }

    /// <summary>
    /// Trigger the object collision by information tidbits
    /// </summary>
    /// <param name="contactPoint"></param>
    ///     The contact point of the bolt
    /// <param name="collider"></param>
    ///     The collider of the collided object
    /// <param name="rigidbody"></param>
    ///     The rigidbody of the collider object
    protected virtual void TriggerObjectCollision
        (Vector3 contactPoint, Collider collider, Rigidbody rigidbody = null)
    {
        //TODO: Get the gameobject
        GameObject collidedGameObject = collider.gameObject;

        print(collider.name);

        //TODO: Trigger any hittable information
        #region Trigger IHitable information
        IHitable hittableInformation = collidedGameObject.GetComponent<IHitable>();
        if (hittableInformation != null)
            hittableInformation.IHit();
        #endregion
    }

    /// <summary>
    /// Triggger the bolt collision
    /// </summary>
    /// <param name="collision"></param>
    ///     The collision information
    protected virtual void TriggerBoltCollision(Collision collision)
    {
        TriggerBoltCollision(collision.contacts[0].point);
    }

    /// <summary>
    /// Do some trigger of the bolt collision
    /// </summary>
    /// <param name="contactPoint"></param>
    ///     The contact point of the bolt collision
    protected virtual void TriggerBoltCollision(Vector3 contactPoint)
    {
        this.IHit();
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
