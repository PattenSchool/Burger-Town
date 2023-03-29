using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private void Update()
    {
        if (PlayerStatic.Player != null)
            this.transform.LookAt(PlayerStatic.Player.transform.position);
    }
}
