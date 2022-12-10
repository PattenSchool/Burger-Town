using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightManager : MonoBehaviour
{
    [SerializeField]
    private DistractableObject distractableObject;

    [SerializeField]
    private GameObject[] ingredients;

    [SerializeField]
    private Vector3 distractableObjectSpawn;

    private void Start()
    {
        distractableObjectSpawn = distractableObject.gameObject.transform.position;
        
    }

    private void Update()
    {
        if (!IsIngredientsActive())
        {
            SpawnDistractableObject();
        }
    }

    public void SpawnDistractableObject()
    {
        foreach(var ingredient in ingredients)
        {
            if (ingredient.activeInHierarchy == true)
            {
                return;
            }
        }
        if (distractableObject.gameObject.activeInHierarchy == true)
        {
            return;
        }


        GameObject obj = distractableObject.gameObject;

        obj.SetActive(true);

        obj.transform.position = distractableObjectSpawn;

    }

    public void ResetIngredience()
    {
        foreach(var ingredient in ingredients)
        {
            ingredient.SetActive(true);
        }
    }

    public bool IsIngredientsActive()
    {
        foreach (var ingredient in ingredients)
        {
            if (ingredient.gameObject.activeInHierarchy == true)
            {
                return true;
            }
        }
        return false;
    }
}
