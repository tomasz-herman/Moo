using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    private static readonly System.Random random = new System.Random();

    public static int NumberBetween(int i, int j) { return random.Next(i, j + 1); }
    public static float FloatBetween(float i, float j) { return (float)random.NextDouble() * (j - i) + i; }
    public static int RandomNumber() { return random.Next(); }

    public static float RandomGaussNumber(float mean, float stdev)
    {
        var U1 = (float)random.NextDouble();
        var U2 = (float)random.NextDouble();

        return stdev * Mathf.Sqrt(-2 * Mathf.Log(U1, Mathf.Exp(1))) * Mathf.Cos(2 * Mathf.PI * U2) + mean;
    }
}
