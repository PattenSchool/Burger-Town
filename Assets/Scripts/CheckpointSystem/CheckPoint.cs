using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField]
    public int index;

    #region Unity Methods
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == PlayerStatic.PlayerTag)
            CheckPointManager.instance.RegisterCheckPoint(this);
    }
    #endregion
}
