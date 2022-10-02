using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTesting : MonoBehaviour, IHitable
{
    private void OnCollisionEnter(Collision collision)
    {
        IHit();
    }

    public void IHit()
    {
        
    }
}
