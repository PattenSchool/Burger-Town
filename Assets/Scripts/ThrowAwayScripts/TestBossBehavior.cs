using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBossBehavior : MonoBehaviour
{

    public Animator bossAnimation;

    private void OnCollisionEnter(Collision collision)
    {
        //Plays the idle animation if the Idle Test Object is touched.
        if (this.gameObject.name.ToString() == "Idle (UnityEngine.GameObject)")
        {
            bossAnimation.Play("BossIdle");
            Debug.Log("Idle animation is playing");
        }

        //Plays the hostile animation if the Hostile Test Object is touched.
        if (this.gameObject.name.ToString() == "Hostile (UnityEngine.GameObject)")
        {
            bossAnimation.Play("BossHostile");
            Debug.Log("Hostile animation is playing");
        }

        //Plays the hostile into idle animation if the Hostile Into Idle Test Object is touched.
        if (this.gameObject.name.ToString() == "Hostile Into Idle (UnityEngine.GameObject)")
        {
            bossAnimation.Play("BossHostileEnterInto");
            Debug.Log("Hostile into Idle animation is playing");
        }

        //Plays the distracted animation if the Distracted Test Object is touched.
        if (this.gameObject.name.ToString() == "Distracted (UnityEngine.GameObject)")
        {
            bossAnimation.Play("BossDistracted");
            Debug.Log("Distracted animation is playing");
        }

        //Plays the death animation if the Death Test Object is touched.
        if (this.gameObject.name.ToString() == "Death (UnityEngine.GameObject)")
        {
            bossAnimation.Play("BossDeath");
            Debug.Log("Death animation is playing");
        }

    }
}
