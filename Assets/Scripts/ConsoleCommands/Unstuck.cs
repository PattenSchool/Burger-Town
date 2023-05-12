using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unstuck : ConsoleCommand
{
    public AudioClip clip;

    public override string Run()
    {
        CheckPointManager.instance.RespawnPlayer();

        AudioManager.instance.PlaySFX(clip);

        return "Player moved";
    }

    public override string RunParameter(float parameter)
    {
        return "Command does not accept parameter";
    }
}
