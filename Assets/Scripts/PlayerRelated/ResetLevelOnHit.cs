using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevelOnHit : MonoBehaviour, IHitable
{
    public LayerMask boltLayer;

    public void IHit()
    {
        print("Success");

        Scene activeScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(activeScene.name);
    }

    public void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.layer == boltLayer)
        {
            print("Success");

            Scene activeScene = SceneManager.GetActiveScene();

            SceneManager.LoadScene(activeScene.name);


        }
    }
}
