using UnityEngine;

public class StickyBoltMechanics : BoltTemplate
{
    #region Platform Variables
    [Header("Platform Var")]

    [Tooltip("The platform that spawns from the sticky bolt")]
    [SerializeField]
    private GameObject _spawnedPlatform;

    [Tooltip("Timer for the platform")]
    [SerializeField]
    private float _platformTimer = 5f;

    [Tooltip("The layermask of the non-sticky things")]
    [SerializeField]
    private LayerMask _nonStickyLayerMasks;

    /// <summary>
    /// Set up the platform that allows the player to jump
    /// </summary>
    /// <param name="spawnPoint"></param>
    ///     Where the platform will spawn
    private void SpawnPlatform(Vector3 spawnPoint)
    {
        //Set up the rotation
        Vector3 platformRotation = new Vector3(0f, this.transform.rotation.y, 0f);
        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = platformRotation;

        //Spawn the platform
        GameObject platform = ObjectPooling.Spawn(_spawnedPlatform, spawnPoint, rotation);

        //Set the desired time
        _spawnedPlatform.GetComponent<StickyBoltPlatormTimer>().DespawnWithTimer(_platformTimer);
    }
    #endregion

    #region Collision Methods
    /// <summary>
    /// The function that makes the sticky bolt function
    /// </summary>
    public override void IHit()
    {
        ToggleStick(true);

        base.IHit();
    }

    /// <summary>
    /// Used to find the normal of the contact point and 
    ///     make the sticky bolt parallel to the ground
    /// </summary>
    /// <param name="collision"></param>
    protected void OnCollisionEnter(Collision collision)
    {
        //Have no idea what's going on here, thanks Stack Overflow
        bool onNonstickSurface =
            (_nonStickyLayerMasks & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer;

        //Spawn the platform if the material is compatible
        if (!onNonstickSurface)
        {
            ContactPoint contactPoint = collision.contacts[0];

            SpawnPlatform(contactPoint.point);
        }

        //Apply the IHit method
        IHit();
    }

    /// <summary>
    /// Functionality to reset the stick and set fire velocity
    /// </summary>
    /// <param name="firee"></param>
    /// <param name="directionVector"></param>
    public override void OnFire(GameObject firee, Vector3 directionVector)
    {
        //Reset the stick state
        ToggleStick(false);

        //Do the base fire action
        base.OnFire(firee, directionVector);
    }

    /// <summary>
    /// Sets the stickyness of the bolt
    /// </summary>
    /// <param name="isStick"></param>
    ///     If the rigidbody to act if it is stuck on an object or not
    private void ToggleStick(bool isStick)
    {
        //If sticky is true
        if (isStick == true)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.useGravity = false;
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        //If stick is false
        else if (isStick == false)
        {
            _rigidbody.useGravity = true;
            _rigidbody.constraints = RigidbodyConstraints.None;
        }
    }
    #endregion
}
