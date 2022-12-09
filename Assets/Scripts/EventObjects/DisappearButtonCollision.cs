using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearButtonCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameObject otherObject = collision.gameObject;
        if (otherObject.tag == "Player")
        {
            DisappearBlockMesh.meshVisible = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        GameObject otherObject = collision.gameObject;
        if (otherObject.tag == "Player")
        {
            DisappearBlockMesh.meshVisible = false;
        }
    }
}
