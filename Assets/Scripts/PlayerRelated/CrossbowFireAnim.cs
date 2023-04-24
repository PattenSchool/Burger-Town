using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowFireAnim : MonoBehaviour
{
    public Animation fire;

    public BoltTemplate[] bolts;

    public AudioClip fireSFX;

    public ShootScript _shootScript;

    private BoltTemplate currentBolt;

    // Start is called before the first frame update
    void Start()
    {
        PlayerStatic.Player.GetComponent<ShootScript>().fireAnimScript = this;

        _shootScript = PlayerStatic.Player.GetComponent<ShootScript>();

        SetBoltModel();
    }

    // Update is called once per frame
    void Update()
    {
        if (_shootScript.isFired() == true)
        {
            currentBolt.gameObject.SetActive(false);
        }
        else 
            currentBolt.gameObject.SetActive(true);
    }

    public void PlayFireAnim()
    {
        fire.Play();
        //currentBolt.gameObject.SetActive(false);
    }

    public void SetBoltModel()
    {
        foreach (BoltTemplate bolt in bolts)
        {
            if (bolt.name != _shootScript.GetSelectedBolt().name)
            {
                bolt.gameObject.SetActive(false);
            }
            else
            {
                bolt.gameObject.SetActive(true);
                currentBolt = bolt;
            }
        }
    }
}
