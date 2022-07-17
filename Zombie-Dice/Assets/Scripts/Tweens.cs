using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tweens
{
    public static float SmoothStart(float t)
    {
        return t * t;
    }

    public static float SmoothStop(float t)
    {
        return 1 - (1 - t) * (1 - t);
    }

    public static float SmoothStep(float t)
    {
        return (1 - t) * SmoothStart(t) + t * SmoothStop(t);
    }
}
