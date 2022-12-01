using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainToLoadMenu : MonoBehaviour
{
    [SerializeField]
    private Canvas loadMenu;

    [SerializeField]
    private Canvas mainMenu;

    public void LoadLoadMenu()
    {
        loadMenu.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
    }
}
