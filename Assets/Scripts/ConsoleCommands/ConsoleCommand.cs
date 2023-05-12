using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConsoleCommand : MonoBehaviour
{
    public abstract string Run();

    public abstract string RunParameter(float parameter);
}
