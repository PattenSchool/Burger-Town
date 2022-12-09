using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeLinkObject : MonoBehaviour, IHitable
{
    /// <summary>
    /// What happens if 
    /// </summary>
    public void IHit()
    {
        this.gameObject.GetComponent<Collider>().enabled = false;
        this.gameObject.SetActive(false);
    }
}
