using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipLevel : ConsoleCommand
{
    public override string Run()
    {
        LevelManagerStatic.IncrementLevel();

        return "";
    }

    public override string RunParameter(float parameter)
    {
        return "Command does not accept parameter";
    }
}
