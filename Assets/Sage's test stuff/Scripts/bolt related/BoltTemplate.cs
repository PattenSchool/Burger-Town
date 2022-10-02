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
    #endregion

    #region Time Variables
    [Header("Time Variables")]

    [Tooltip("The desired time bolt to exist in the world in seconds")]
    [SerializeField, Min(0f)]
    private float _desiredSetTime = 0f;
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
