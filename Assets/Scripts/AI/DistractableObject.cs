using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractableObject : MonoBehaviour, IHitable
{
    /// <summary>
    /// Destroy itself if hit by bolt
    /// </summary>
    public void IHit()
    {
        this.gameObject.SetActive(false);
    }
}
