using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer
{
    public static T GetRandom<T>(List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }
}
