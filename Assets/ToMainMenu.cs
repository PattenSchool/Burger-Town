using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMainMenu : MonoBehaviour
{
    [SerializeField]
    public static void ToTheMain()
    {
        SceneManager.LoadScene(0);
        print("ghj");
    }
}
