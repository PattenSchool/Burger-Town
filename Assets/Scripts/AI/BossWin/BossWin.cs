using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWin : MonoBehaviour
{
    [SerializeField]
    private LifeLinkObject[] lifeLinkObjects;

    [SerializeField]
    private GameObject winButton;
    private void Start()
    {
        lifeLinkObjects = GameObject.FindObjectsOfType<LifeLinkObject>();
        winButton.SetActive(false);
    }

    private void Update()
    {
        if (!isAlive())
        {
            winButton.SetActive(true);
        }
    }

    private bool isAlive()
    {
        foreach(var obj in lifeLinkObjects)
        {
            if (obj.gameObject.activeInHierarchy == true)
                return true;
        }

        return false;
    }
}
