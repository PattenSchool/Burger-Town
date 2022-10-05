using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Threading : MonoBehaviour
{
    void Start()
    {

        Debug.Log("Start() :: Starting.");

        Thread myThread = new Thread( SlowJob );

        myThread.Start();

        Debug.Log("Start() :: Done.");
    }

    bool isRunning = false;

    void Update()
    {
        if (isRunning)
        {
            Debug.Log("SlowJob isRunning");

           // this.transform.Translate( Vector3.up * Time.deltaTime );
        }
    }

    void SlowJob() {
        isRunning = true;

        Debug.Log("Threading::SlowJob() -- Doing 1000 things, each taking 2ms");

        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        sw.Start();

        for (int i = 0; i < 1000; i++)
        {
            this.transform.Translate( Vector3.up * 0.002f );
            Thread.Sleep(2);
        }

        sw.Stop();

        Debug.Log("Threading::SlowJob() -- Done! Elapsed time: " + sw.ElapsedMilliseconds/1000f);

        isRunning = false;
    }
}
