using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResetLevel_Collider : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == PlayerStatic.PlayerTag)
            LevelManagerStatic.ResetLevel();
    }

    private void Update()
    {
        
    }
}
