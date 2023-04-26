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

    public bool resetLauncherBolt;

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
        if (_shootScript.timeRemaining <= 0.10f)
        {
            if (!PlayerStatic.IsGrounded && _shootScript.currentBoltIndex == 3)
            {
                //currentBolt.gameObject.SetActive(false);
            }
            else
                currentBolt.gameObject.SetActive(true);
        }
        else
            currentBolt.gameObject.SetActive(false);

        if (_shootScript.isFired() && _shootScript.currentBoltIndex == 3)
        {
            resetLauncherBolt = false;
        }

        if (PlayerStatic.IsGrounded)
        {
            resetLauncherBolt = true;
        }
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
                if (!PlayerStatic.IsGrounded && _shootScript.currentBoltIndex == 3
                    && !resetLauncherBolt)
                {
                    bolt.gameObject.SetActive(false);
                }
                else
                {
                    bolt.gameObject.SetActive(true);
                }
                currentBolt = bolt;
            }
        }
    }
}
