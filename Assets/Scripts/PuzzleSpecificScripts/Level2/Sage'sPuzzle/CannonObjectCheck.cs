using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonObjectCheck : MonoBehaviour
{
    public MoveObject _moveObject;

    public float safeTime = 0.10f;

    private float duration = 0f;

    private string originalTag;

    public bool isExplosive;

    public GameObject wallToDestroy;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        originalTag = this.gameObject.tag;

        /*
        if (this.gameObject.GetComponent<Explosive>() != null)
        {
            isExplosive = true;
        }
        else
        {
            isExplosive = false;
        }
        */

        if (wallToDestroy == null)
        {
            wallToDestroy = new GameObject();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_moveObject.isCannonFired)
        {
            duration += Time.deltaTime;

            if (duration >= (_moveObject.duration + safeTime))
            {
                if (isExplosive == true)
                {
                    wallToDestroy.SetActive(false);

                    this.gameObject.SetActive(false);
                }
                else
                {
                    this.gameObject.tag = originalTag;

                    rb.useGravity = true;
                }
            }
        }


    }
}
