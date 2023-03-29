using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnWithHit : MonoBehaviour, IHitable
{
    public void IHit()
    {
        ObjectPooling.Despawn(this.gameObject);
    }
}
