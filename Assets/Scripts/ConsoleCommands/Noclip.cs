using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noclip : ConsoleCommand
{
    private bool isDebug;

    public override string Run()
    {
        isDebug = PlayerStatic.Player.GetComponent<rbCharacterController>().isDebug;

        PlayerStatic.Player.GetComponent<rbCharacterController>().ToggleDebug();

        if (isDebug != PlayerStatic.Player.GetComponent<rbCharacterController>().isDebug)
        {
            isDebug = PlayerStatic.Player.GetComponent<rbCharacterController>().isDebug;

            if (isDebug)
            {
                return ("Noclip enabled");
            }
            else
            {
                return ("Noclip disabled");
            }
        }
        else
        {
            return ("Noclip not enabled check the player");
        }
    }

    public override string RunParameter(float parameter)
    {
        return "Command does not accept parameter";
    }
}
