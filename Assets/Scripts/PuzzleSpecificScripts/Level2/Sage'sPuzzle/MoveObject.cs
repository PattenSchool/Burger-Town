using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveObject : MonoBehaviour
{
    public bool isPlayerCannon;
    public bool isToDestroyWall;

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
                StopCoroutine(moveObject);
                moveObject = null;

                if (projectile.tag == PlayerStatic.PlayerTag)
                {
                    PlayerStatic.Player.GetComponent<rbCharacterController>().isLaunchedByCannon = false;
                }

                if (isToDestroyWall && projectile.GetComponent<Explosive>() != null)
                {
                    if (projectile.GetComponent<Explosive>().explosiveIsActive)
                    {
                        end.gameObject.SetActive(false);
                        projectile.SetActive(false);
                    }
                }
                else
                {
                    projectile.tag = projectileTag;
                    projectile.GetComponent<Rigidbody>().useGravity = true;
                }

                if (projectile.gameObject.GetComponent<Explosive>() != null)
                {
                    projectile.gameObject.GetComponent<Explosive>().explosiveIsActive = false;
                }

                projectile = null;
            }
            else if (projectile.tag == PlayerStatic.PlayerTag)
            {
                if (PlayerStatic.Player.GetComponent<rbCharacterController>().isLaunchedByCannon == false)
                {
                    StopCoroutine(moveObject);

                    moveObject = null;

                    projectile.tag = projectileTag;
                    projectile.GetComponent<Rigidbody>().useGravity = true;

                    projectile = null;
                }
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isPlayerCannon)
        {
            if (other.gameObject.tag == PlayerStatic.PlayerTag)
            {
                //PlayerStatic.Player.GetComponent<GrabObject>().ClearGrabObject();

                PlayerStatic.Player.GetComponent<ShootScript>().isLaunching = false;

                projectile = other.gameObject;
                projectileTag = other.gameObject.tag;

                projectile.GetComponent<Rigidbody>().useGravity = false;

                PlayerStatic.Player.GetComponent<rbCharacterController>().isLaunchedByCannon = true;

                moveObject = StartCoroutine(Move(projectile));
            }
        }
        else
        {
            if (other.gameObject.tag == "PhysObject")
            {
                if (other.gameObject.GetComponent<Explosive>() != null)
                {
                    other.gameObject.GetComponent<Explosive>().explosiveIsActive = true;
                }

                PlayerStatic.Player.GetComponent<GrabObject>().ClearGrabObject();


                projectile = other.gameObject;
                projectileTag = other.gameObject.tag;

                projectile.GetComponent<Rigidbody>().useGravity = false;

                projectile.transform.rotation = this.transform.rotation;

                moveObject = StartCoroutine(Move(projectile));
            }
        }
    }

    private IEnumerator Move(GameObject launchee)
    {
        if (launchee.tag != PlayerStatic.PlayerTag)
        {
            float t = 0;

            while (t < duration)
            {
                launchee.transform.rotation = this.transform.rotation;
                float percent = t / duration;
                t += Time.deltaTime;
                launchee.transform.position = Vector3.Lerp(start.position, end.position, percent) + Vector3.up * curve.Evaluate(percent);
                yield return null;
            }
        }
        else
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
    }
}