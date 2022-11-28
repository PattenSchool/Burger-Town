using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeakSpot : MonoBehaviour
{

    public MeshRenderer normalMesh;
    public MeshRenderer damageMesh;

    public void IHit()
    {
        // Cannot get IHit to work here, but should function the same as the OnCollisionEnter method.
        Debug.Log("Boss Hit");
        StartCoroutine(hurtBoss());
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Starts the hurtBoss coroutine animation when the weak spot is hit.
        Debug.Log("Boss Hit");
        StartCoroutine(hurtBoss());
    }

    IEnumerator hurtBoss()
    {
        // Turns off the boss' normal body mesh and turns on his hurt mesh
        normalMesh.enabled = false;
        damageMesh.enabled = true;

        //Pauses for a second.
        yield return new WaitForSeconds(1F);

        //Turns the boss' normal body mesh back on and the hurt mesh back off.
        normalMesh.enabled = true;
        damageMesh.enabled = false;

    }
}
