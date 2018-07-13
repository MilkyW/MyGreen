
using UnityEngine;

namespace SpringMesh
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class HeatMap : MonoBehaviour
    {
        private float[,] temperature = null;
        private int horizontal = 0;
        private int vertical = 0;
        private MeshFilter meshFilter = null;
        private Vector3 size;

        private void Awake()
        {
            meshFilter = this.GetComponent<MeshFilter>();
            size = this.transform.localScale;
        }

        private Mesh DrawHeatMap()
        {
            Mesh mesh = new Mesh();
            mesh.name = "HeatMap";

            Vector3[] vertices = new Vector3[horizontal * vertical];
            Color[] colors = new Color[vertices.Length];
            Vector2[] uvs = new Vector2[vertices.Length];
            int[] triangles = new int[( horizontal - 1 ) * ( vertical - 1 ) * 6];

            Vector3 origin = new Vector3(-size.x / 2.0f , -size.y / 2.0f , 0); 
            float perWidth = size.x / horizontal;
            float perHeight = size.y / vertical;
            
            // vertices
            // uvs 
            // colors
            for ( int j = 0; j < vertical; j++)
            {
                for (int i = 0; i < horizontal; i++)
                {
                    Vector3 vertex = origin + new Vector3(i * perWidth, j * perHeight, 0);
                    vertices[horizontal * j + i] = vertex;
                    uvs[horizontal * j + i] = new Vector2(0 , 1) + new Vector2(1 / horizontal * i , 1 / vertical * j);
                    //colors[horizontal * j + i] = CalcColor(this.temperature[j , i]);
                }
            }

            // triangles
            int count = 0;
            for ( int i = 0 ; i < vertical - 1; i++ )
            {
                for ( int j = 0 ; j < horizontal -1 ; j++ )
                {
                    triangles[count] = i * horizontal + j;
                    triangles[count + 1] = ( i + 1 ) * horizontal + j;
                    triangles[count + 2] = i * horizontal + j + 1;
                    triangles[count + 3] = i * horizontal + j + 1;
                    triangles[count + 4] = ( i + 1 ) * horizontal + j;
                    triangles[count + 5] = ( i + 1 ) * horizontal + j + 1;
                    count += 6;
                }
            }

            mesh.vertices = vertices;
            mesh.colors = colors;
            mesh.uv = uvs;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
            return mesh;
        }

        private void AddVertexColor( )
        {
            Color[] colors = new Color[meshFilter.mesh.colors.Length];
            for ( int j = 0 ; j < vertical ; j++ )
            {
                for ( int i = 0 ; i < horizontal ; i++ )
                    colors[horizontal * j + i] = CalcColor(this.temperature[j , i]);
            }
            meshFilter.mesh.colors = colors;
        }

        private Color CalcColor( float temperature )  
        {
            int count = (int)temperature / 10;
            float temp = ( temperature % 10 ) / 10;
            Color[] colors = GetColors(count);
            Color from = colors[0];
            Color to = colors[1];
            Color offset = to - from;
            return from + offset * temp;
        }

        // set color by your need
        private Color[] GetColors( int index )
        {
            Color startColor = Color.blue, endColor = Color.blue;
            switch ( index )
            {
                // 10~20 color
                case 1:
                    break;
                // 20~30 color
                case 2:
                    break;
                // 30~40 color
                case 3:
                    break;
                // 40~50 color
                case 4:
                    break;
                // 50~60 color
                case 5:
                    startColor = Color.blue;
                    endColor = Color.cyan;
                    break;
                // 60~70 color
                case 6:
                    startColor = Color.cyan;
                    endColor = Color.green;
                    break;
                // 70~80 color
                case 7:
                    startColor = Color.green;
                    endColor = Color.yellow;
                    break;
                // 80~90 color
                case 8:
                    startColor = Color.yellow;
                    endColor = Color.red;
                    break;
                // 90~100 color
                case 9:
                    startColor = Color.red;
                    endColor = Color.magenta;
                    break;
                // over 100 color
                case 10:
                    startColor = Color.magenta;
                    endColor = Color.magenta;
                    break;
            }
            return new Color[] { startColor , endColor };
        }

        public void Inject( float[,] temperature )
        {
            this.temperature = temperature;
            this.horizontal = temperature.GetLength(1);
            this.vertical = temperature.GetLength(0);
            RandomTeamperature(99 , 50 , 0 , 45 , ref this.temperature);
            RandomTeamperature(89 , 50 , 0 , 25 , ref this.temperature);
            RandomTeamperature(79 , 50 , 0 , 30 , ref this.temperature);
            RandomTeamperature(69 , 50 , 0 , 33 , ref this.temperature);
            RandomTeamperature(89 , 50 , 0 , 50 , ref this.temperature);
            RandomTeamperature(79 , 50 , 0 , 22 , ref this.temperature);
            meshFilter.mesh = DrawHeatMap();
            AddVertexColor();
        }

        private void RandomTeamperature( float from , float to, int minD , int maxD , ref float[,] temperatures ) 
        {
            int randomX = Random.Range(3 , horizontal);
            int randomY = Random.Range(3 , vertical);

            float maxTweenDis = maxD - minD;
            float offset = to - from;
            for ( int i = randomX - maxD ; i < randomX + maxD ; i++ )
            {
                for ( int j = randomY + maxD ; j > randomY - maxD ; j-- )
                {
                    if ( i < 0 || i >= horizontal )
                        continue;
                    if ( j < 0 || j >= vertical )
                        continue;  
                    float distance = Mathf.Sqrt(Mathf.Pow(randomX - i , 2) + Mathf.Pow(randomY - j , 2));
                    if ( distance <= maxD && distance >= minD )
                    {
                        float offsetDis = distance - minD;
                        float ratio = offsetDis / maxTweenDis;
                        float temp = from + ratio * offset;
                        if ( temp > temperatures[i , j] )
                            temperatures[i , j] = temp;
                    }
                }
            }
        }

        private void TemperatureToString( )
        {
            for ( int y = 0 ; y < vertical ; y++ )
            {
                string input = "";
                for ( int x = 0 ; x < horizontal ; x++ )
                {
                    input += this.temperature[y , x].ToString("F2") + ",";
                }
                Debug.Log(input);
            }
        }
    }
}