using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public AnimationCurve curve;

    private Transform start;
    public Transform end;
    public float duration;
    public float radius;


    private GameObject projectile;
    private string projectileTag;

    // Start is called before the first frame update
    void Start()
    {
        start = this.transform;
        //StartCoroutine(Move());
    }

    private void Update()
    {
        if (projectile != null)
        {
            if (Vector3.Distance(projectile.transform.position, end.transform.position) < radius)
            {
                projectile.tag = projectileTag;
                projectile.GetComponent<Rigidbody>().useGravity = true;

                projectile = null;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PhysObject")
        {
            PlayerStatic.Player.GetComponent<GrabObject>().ClearGrabObject();


            projectile = other.gameObject;
            projectileTag = other.gameObject.tag;

            projectile.GetComponent<Rigidbody>().useGravity = false;

            StartCoroutine(Move(projectile));
        }
    }


    private IEnumerator Move(GameObject launchee)
    {
        float t = 0;

        while(t < duration)
        {
            float percent = t / duration;
            t += Time.deltaTime;
            launchee.transform.position = Vector3.Lerp(start.position, end.position, percent) + Vector3.up * curve.Evaluate(percent);
            yield return null;
        }
    }
}
