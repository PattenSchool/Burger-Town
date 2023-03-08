using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resettable : MonoBehaviour, IResettable
{
    public GameObject IgameObject { get; set; }

    public Vector3 IStoredPosition { get; set; }
    public Quaternion IStoredRotation { get; set; }

    private void Start()
    {
        IgameObject = this.gameObject;

        IStoreOriginalTransform();
    }

    public void IStoreOriginalTransform()
    {
        IStoredPosition = this.transform.position;
        IStoredRotation = this.transform.rotation;
    }
    public void IResetTransform()
    {
        transform.position = IStoredPosition;
        transform.rotation = IStoredRotation;
    }
}
