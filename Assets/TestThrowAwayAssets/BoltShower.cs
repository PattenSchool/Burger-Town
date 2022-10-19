using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoltShower : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _boltUIText;

    [SerializeField]
    private SchootScript shootScript;

    private void Awake()
    {
        shootScript = GetComponent<SchootScript>();
    }

    private void Update()
    {
        int boltindex = shootScript.currentBoltIndex;
        _boltUIText.text = shootScript.boltPrefabs[boltindex - 1].name;
    }
}
