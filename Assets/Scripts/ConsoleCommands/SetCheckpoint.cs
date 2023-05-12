using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SetCheckpoint : ConsoleCommand
{
    private List<CheckPoint> localPoints = new List<CheckPoint> ();

    public override string Run()
    {
        return "Index required";
    }

    public override string RunParameter(float parameter)
    {
        localPoints = CheckPointManager.instance.checkpoints;

        int index = 0;

        index += Convert.ToInt32(parameter);

        if (index > -1 && index < localPoints.Count)
        {
            CheckPointManager.instance.RegisterCheckPoint(localPoints[index]);
            CheckPointManager.instance.RespawnPlayer();

            return "Checkpoint loaded";
        }
        else
        {
            return "Could not find checkpoint";
        }
    }
}
