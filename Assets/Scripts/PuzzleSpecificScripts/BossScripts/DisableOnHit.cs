using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnHit : MonoBehaviour, IHitable
{
    public void IHit()
    {
        this.gameObject.SetActive(false);
    }
}
