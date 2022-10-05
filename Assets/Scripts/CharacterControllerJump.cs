using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Analytics;

public class CharacterControllerJump : MonoBehaviour
{
    private int level = 1;
    public float moveSpeed;
    public float jumpForce;
    public CharacterController controller;

    private Vector3 _moveDirection;
   // private PhotonView _view;
    public float gravityScale;

    void Start() {
        controller = GetComponent<CharacterController>();
   //     _view = GetComponent<PhotonView>();
    }

    void Update() {
      //  if (_view.IsMine)
      //  {

 _moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, _moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);

if (controller.isGrounded) {
        if (Input.GetButtonDown("Jump"))
        {
            _moveDirection.y = jumpForce;
        }
    }
        _moveDirection.y = _moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        controller.Move(_moveDirection * Time.deltaTime);

       // }
       
    }

private void OnTriggerEnter(Collider collision) {
    if(collision.tag == "Win!")
    {
        Analytics.CustomEvent("WinGameScript");
        Debug.Log("Win!");
    }

    if(collision.tag == "Die!")
    {
        Analytics.CustomEvent("LoseGameScript");
        Debug.Log("Die!");
    }
}
}

