using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static float EaseInOutCubic(float x)
    {
        return x < 0.5 ? 4 * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 3) / 2;
    }
}