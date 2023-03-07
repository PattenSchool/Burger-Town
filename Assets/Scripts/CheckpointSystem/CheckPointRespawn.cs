using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointRespawn : MonoBehaviour
{
    private void Start()
    {
        CheckPointManager.instance.RespawnPlayer();
    }
}
