using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceMusicReset : MonoBehaviour
{
    #region Game Obejct
    [Header("Game objects")]

    [Tooltip("The musci box being reset")]
    [SerializeField]
    private GameObject musicBox;
    #endregion


    private void Start()
    {
        StartCoroutine(ForceStart());
    }
    private IEnumerator ForceStart()
    {
        musicBox.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        musicBox.SetActive(true);
    }
}
