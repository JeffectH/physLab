using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lab5Physycs.Utils
{
    public static class RandomUtils
    {
       public static float GetRandomValueAndZnak(float value)
        {
            return Random.Range(-value, value);
        }
    }
}
