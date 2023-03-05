using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class CheckPointReset : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        DestroyCheckPointManager(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        DestroyCheckPointManager(collision.gameObject);
    }

    private void DestroyCheckPointManager(GameObject go)
    {
        if (go.tag == PlayerStatic.Player.tag)
            Destroy(ObjectSave.instance.gameObject);
    }
}
