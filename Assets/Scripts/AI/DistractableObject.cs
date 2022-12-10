using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractableObject : MonoBehaviour, IHitable
{
    public BossFightManager fightManager;

    public GameObject deathBox;

    /// <summary>
    /// Destroy itself if hit by bolt
    /// </summary>
    public void IHit()
    {
        fightManager.ResetIngredience();
        this.gameObject.SetActive(false);

    }

    private void Update()
    {
        if (this.gameObject.transform.position.y  <= deathBox.transform.position.y)
        {
            this.gameObject.SetActive(false);
        }
    }
}
