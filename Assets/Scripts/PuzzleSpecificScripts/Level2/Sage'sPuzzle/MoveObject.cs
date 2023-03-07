using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public bool isPlayerCannon;

    public AnimationCurve curve;

    private Transform start;
    public Transform end;
    public float duration;
    public float radius;


    private GameObject projectile;
    private string projectileTag;

    private Coroutine moveObject;

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
                if (projectile.tag == PlayerStatic.PlayerTag)
                {
                    PlayerStatic.Player.GetComponent<rbCharacterController>().isLaunchedByCannon = false;
                }

                projectile.tag = projectileTag;
                projectile.GetComponent<Rigidbody>().useGravity = true;

                projectile = null;
            }

            else if (projectile.tag == PlayerStatic.PlayerTag)
            {
                if (PlayerStatic.Player.GetComponent<rbCharacterController>().isLaunchedByCannon == false)
                {
                    StopCoroutine(moveObject);

                    projectile.tag = projectileTag;
                    projectile.GetComponent<Rigidbody>().useGravity = true;

                    projectile = null;
                }
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

            projectile.transform.rotation = this.transform.rotation;

            moveObject = StartCoroutine(Move(projectile));
        }
        else if (other.gameObject.tag == PlayerStatic.PlayerTag)
        {
            //PlayerStatic.Player.GetComponent<GrabObject>().ClearGrabObject();


            projectile = other.gameObject;
            projectileTag = other.gameObject.tag;

            projectile.GetComponent<Rigidbody>().useGravity = false;

            PlayerStatic.Player.GetComponent<rbCharacterController>().isLaunchedByCannon = true;

            moveObject = StartCoroutine(Move(projectile));
        }
    }

    private void ClearProjectile()
    {
        projectile.tag = projectileTag;
        projectile.GetComponent<Rigidbody>().useGravity = true;

        projectile = null;
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

    /*
    private IEnumerator MovePlayer(GameObject launchee)
    {
        print(launchee.GetComponent<rbCharacterController>().isLaunchedByCannon);
        if (PlayerStatic.Player.GetComponent<rbCharacterController>().isLaunchedByCannon)
        {
            float t = 0;

            while (t < duration)
            {
                float percent = t / duration;
                t += Time.deltaTime;
                launchee.transform.position = Vector3.Lerp(start.position, end.position, percent) + Vector3.up * curve.Evaluate(percent);
                yield return null;
            }
        }
        else
        {
            print("test");
            ClearProjectile();
            yield return null;
        }
    }
    */
}
