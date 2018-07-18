using UnityEngine;
using SpringMesh;

public class HeatMapExample : MonoBehaviour
{
    public int horizontal = 40;
    public int vertical = 40;

    public HeatMap Heatmap = null;

    private void Awake()
    {
        Heatmap.Inject(InitTemperatures()); 
    }

    private float[,] InitTemperatures()  
    {
        float[,] temperature = new float[vertical , horizontal];
        for (int j = 0; j < vertical ; j++)
            for ( int i = 0 ; i < horizontal ; i++)
                temperature[j , i] = 50;
        return temperature;
    }
}
