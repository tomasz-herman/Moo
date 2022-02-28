using UnityEngine;

public static class Utils
{
    private static readonly System.Random Random = new System.Random();

    public static int NumberBetween(int i, int j) { return Random.Next(i, j + 1); }
    public static float FloatBetween(float i, float j) { return (float)Random.NextDouble() * (j - i) + i; }
    public static int RandomNumber() { return Random.Next(); }
    public static bool RandomBool() { return (Random.Next() & 1) == 0; }

    public static float RandomGaussNumber(float mean, float stdev)
    {
        var U1 = (float)Random.NextDouble();
        var U2 = (float)Random.NextDouble();

        return stdev * Mathf.Sqrt(-2 * Mathf.Log(U1, Mathf.Exp(1))) * Mathf.Cos(2 * Mathf.PI * U2) + mean;
    }

    public static float RandomTriangular(float min, float med, float max)
    {
        float f = (med - min) / (max - min);
        float rnd = FloatBetween(0, 1);
        if (rnd < f)
        {
            return min + Mathf.Sqrt(rnd * (max - min) * (med - min));
        }
        else
        {
            return max - Mathf.Sqrt((1 - rnd) * (max - min) * (max - med));
        }
    }

    public static Color CombineColors(params Color[] colors)
    {
        var result = new Color(0, 0, 0, 0);
        foreach (var color in colors)
        {
            result += color;
        }
        result /= colors.Length;
        return result;
    }
}
