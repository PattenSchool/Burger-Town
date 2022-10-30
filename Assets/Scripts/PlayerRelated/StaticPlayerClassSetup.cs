using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Only used with setting up the PlayerStatic class
/// </summary>
public class StaticPlayerClassSetup : MonoBehaviour
{
    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag(PlayerStatic.PlayerTag);
        PlayerStatic.SetupPlayer(player);

        if (PlayerStatic.IsExtraneousPlayers())
        {
            Debug.LogError(PlayerStatic.PlayerWarning());
        }
    }
}
