using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CameraRotation : MonoBehaviour
{
    [Tooltip("Main camera's parent gameobject")]
    [SerializeField]
    private GameObject cameraAnchor;

    [Tooltip("Rotation speed of camera")]
    [SerializeField]
    private float rotationSpeed;

    private float tempRotation;

    private float currentYRotation = 0f;

    private Vector3 defaultRotation;


    private void Start()
    {
        defaultRotation = cameraAnchor.transform.eulerAngles;

        //currentYRotation = defaultRotation.y;

        //rotationSpeed *= Time.deltaTime;
    }


    private void Update()
    {
        cameraAnchor.transform.Rotate(Vector3.up * (Time.deltaTime + rotationSpeed));
    }
}
