using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltPlatformDestruct : MonoBehaviour, IHitable, IResettable
{
    public GameObject IgameObject
    {
        get; set;

    }
    public void IStoreOriginalTransform() { }
    public Vector3 IStoredPosition
    {
        get; set;
    }

    public Quaternion IStoredRotation
    {
        get;set;
    }

    private void OnEnable()
    {
        if (IgameObject == null)
            IgameObject = this.gameObject;
    }

    public void IResetTransform()
    {
        //ObjectPooling.Despawn(this.gameObject);
    }
    public void IHit()
    {
        ObjectPooling.Despawn(this.gameObject);
    }
}
