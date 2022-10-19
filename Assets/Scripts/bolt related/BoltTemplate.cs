using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltTemplate : Projectile
{
    #region Game objects
    [Header("Game Objects")]

    [Tooltip("The rigid body of this bolt")]
    [SerializeField]
    protected Rigidbody _rigidbody;
    #endregion

    #region Data Variables
    [Header("Collision Variables")]

    [Tooltip("The range in front of the bolt for detection")]
    [SerializeField, Min(0f)]
    private float _detectionRange = 0f;

    [Tooltip("The initial speed of the bolt in meters per second")]
    [SerializeField]
    protected float _initialSpeed = 10f;
    #endregion

    #region Time Variables
    [Header("Time Variables")]

    [Tooltip("The desired time bolt to exist in the world in seconds")]
    [SerializeField, Min(0f)]
    private float _desiredSetTime = 10f;
    #endregion

    #region Unity Methods
    private void OnEnable()
    {
        if (_rigidbody == null)
            _rigidbody = this.gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Define the ray being cast
        Ray ray = new Ray()
        {
            origin = transform.position,
            direction = Vector3.forward,
        };

        //Define how the hit is being set
        RaycastHit hitInfo;

        //Hit physics
        if (Physics.Raycast(ray, out hitInfo, _detectionRange))
        {
            //What is being hit
            IHitable hitable = hitInfo.collider.GetComponent<IHitable>();

            //If hitable, then deploy IHitable
            if (hitable != null)
            {
                //Deploy it for object
                hitable.IHit();

                //Deploy it for bolt
                IHit();
            }
                
        }
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
    public virtual void OnFire(GameObject firee)
    {
        //Get the direction vector
        Vector3 directionVector = Camera.main.transform.forward;

        //Set the speed of the bolt
        GetComponent<Rigidbody>().velocity = directionVector * _initialSpeed;

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
    private IEnumerator DespawWithTimer()
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
    private void DespawnFromPool()
    {
        //Despawn the game object from the object pool
        ObjectPooling.Despawn(this.gameObject);
    }
    #endregion
}
