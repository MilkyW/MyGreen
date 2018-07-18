using UnityEngine;
using SpringMesh;

public class HeatMapExample : MonoBehaviour
{
    public HeatMap Heatmap = null;

    private void Awake()
    {
        Heatmap.Inject(); 
    }

    //private float[,] InitTemperatures()  
    //{
    //    float[,] temperature = new float[vertical , horizontal];
    //    for (int i = 0; i < vertical ; i++)
    //        for ( int j = 0 ; j < horizontal ; j++ )
    //            temperature[i , j] = 50;
    //    return temperature;
    //}
}
