using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Used to display the bolt selected
/// </summary>
public class BoltUIDisplay : MonoBehaviour
{
    [Header("Components")]

    [Tooltip("Bolt Name Display")]
    [SerializeField]
    public TMP_Text _nameDisplay;

    public Image image;

    private void Start()
    {
        _nameDisplay = this.gameObject.GetComponent<TMP_Text>();
    }
    private void Update()
    {
        if (_nameDisplay.text != PlayerStatic.BoltSelected.name)
        {
            _nameDisplay.text = PlayerStatic.BoltSelected.name;
            image.sprite = PlayerStatic.BoltSelected.GetComponent<BoltTemplate>().sprite;
        }
    }
}
