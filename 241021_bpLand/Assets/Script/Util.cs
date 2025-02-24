using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Util
{
    public static float EaseInOutCubic(float x)
    {
        return x < 0.5 ? 4 * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 3) / 2;
    }

    public static Vector2 Vector2NormalizedForJoystick(this Vector2 v)
    {
        float allowRange = 0.85f;
        int multiplyX = v.x < 0 ? -1 : 1;
        int multiplyY = v.y < 0 ? -1 : 1;
        var curVec = v.normalized;
        curVec= curVec.Abs();
        return new Vector2((curVec.x > allowRange ? 1 : 0)* multiplyX, (curVec.y > allowRange ? 1 : 0) * multiplyY);
    }
}