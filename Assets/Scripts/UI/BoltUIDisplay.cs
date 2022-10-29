using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Used to display the bolt selected
/// </summary>
public class BoltUIDisplay : MonoBehaviour
{
    [Header("Components")]

    [Tooltip("Bolt Name Display")]
    [SerializeField]
    public TMP_Text _nameDisplay;

    private void Start()
    {
        _nameDisplay = this.gameObject.GetComponent<TMP_Text>();
    }
    private void Update()
    {
        _nameDisplay.text = PlayerStatic.Player.gameObject.GetComponent<ShootScript>().name;
    }
}
