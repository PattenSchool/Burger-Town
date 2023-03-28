using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResettable
{
    GameObject IgameObject { get; set; }

    Vector3 IStoredPosition { get; set; }
    Quaternion IStoredRotation { get; set; }


    void IResetTransform();

    void IStoreOriginalTransform();
}
