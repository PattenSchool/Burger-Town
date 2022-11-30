using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(fileName = "New Burger", menuName = "Objective/New Burger")]
public class Burger : ScriptableObject
{
    public List<GameObject> allParts;

    /*

    [System.Flags]
    public enum Parts
    {
        Bun_Top = 1,
        Tomato = 2,
        Lettuce = 4,
        Cheese = 8,
        Patty = 16,
        Bun_Bottom = 32
    }
    */
}
