using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrag : MonoBehaviour
{
    #region Drag ammount
    [Header("Drag related")]

    [Tooltip("The ammount of drag being applied to the rigid body")]
    [SerializeField, Range(0f, 1f)]
    private float drag = 0.25f;

    [Tooltip("Storage of previous drag")]
    [SerializeField]
    private float storedDrag = 0f;
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != PlayerStatic.PlayerTag)
        {
            return;
        }

        Rigidbody enteredRigidBody = other.GetComponent<Rigidbody>();
        storedDrag = enteredRigidBody.drag;
        enteredRigidBody.drag = drag;

        print(enteredRigidBody.drag);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != PlayerStatic.PlayerTag)
        {
            return;
        }

        Rigidbody enteredRigidBody = other.GetComponent<Rigidbody>();
        enteredRigidBody.drag = storedDrag;
    }
}
