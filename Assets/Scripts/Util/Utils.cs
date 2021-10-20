using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    private static readonly System.Random random = new System.Random();

    public static int NumberBetween(int i, int j) { return random.Next(i, j + 1); }
    public static float FloatBetween(float i, float j) { return (float)random.NextDouble() * (j - i) + i; }
    public static int RandomNumber() { return random.Next(); }
}
