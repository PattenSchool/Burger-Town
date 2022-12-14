using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoltShower : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _boltUIText;

    [SerializeField]
    private ShootScript shootScript;

    private void Awake()
    {
        shootScript = GetComponent<ShootScript>();
    }

    private void Update()
    {
        int boltindex = shootScript.currentBoltIndex;
        _boltUIText.text = PlayerStatic.BoltSelected.name;
    }
}
