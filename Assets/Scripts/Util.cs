using UnityEngine;

public class Util
{
    public static int SampleGeometricDistribution(float p)
    {
        int k = 0;
        while (true)
        {
            if (Random.Range(0f, 1f) < p)
            {
                break;
            }
            k++;
        }
        return k;
    }
}
