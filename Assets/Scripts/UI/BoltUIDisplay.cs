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
    public UnityEngine.UI.Image NormalBolt;
    public UnityEngine.UI.Image StickyBolt;
    public UnityEngine.UI.Image RecoilBolt;

    private void Start()
    {
        _nameDisplay = this.gameObject.GetComponent<TMP_Text>();
    }
    private void Update()
    {
        _nameDisplay.text = PlayerStatic.BoltSelected.name;
        DisplayBolt();
    }

    public void DisplayBolt()
    {
        // Changes the sprite to the normal bolt
        if (_nameDisplay.text == "NormalBolt")
        {
            NormalBolt.gameObject.SetActive(true);
            StickyBolt.gameObject.SetActive(false);
            RecoilBolt.gameObject.SetActive(false);
        }

        // Changes the sprite to the sticky bolt
        if (_nameDisplay.text == "StickyBolt")
        {
            NormalBolt.gameObject.SetActive(false);
            StickyBolt.gameObject.SetActive(true);
            RecoilBolt.gameObject.SetActive(false);
        }

        // changes the sprite to the recoil bolt
        if (_nameDisplay.text == "RecoilBolt")
        {
            NormalBolt.gameObject.SetActive(false);
            StickyBolt.gameObject.SetActive(false);
            RecoilBolt.gameObject.SetActive(true);
        }
    }


}
