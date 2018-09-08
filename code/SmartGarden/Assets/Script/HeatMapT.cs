using BestHTTP;
using BestHTTP.WebSocket;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace SpringMesh
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class HeatMapT : MonoBehaviour
    {
        private float[,] temperatures = null;
        private float[,] points = null;
        private int horizontal = 0;
        private int vertical = 0;
        private MeshFilter meshFilter = null;
        private Rect size;
        public int bottom = 0;
        public float radius = 0.3f;
        public int positionFX = 10;
        public int ideal = 20;
        public int step = 5;

        WebSocket webSocket;
        m_garden selected;

        private void Awake()
        {
            meshFilter = this.GetComponent<MeshFilter>();
            size = this.GetComponent<RectTransform>().rect;
            positionFX = 10;
            this.horizontal = (int)size.width / positionFX;
            this.vertical = (int)size.height / positionFX;
            points = InitPoints();

            //Inject();
            //test();
            init();
        }

        private void OnEnable()
        {
            test();
            selected = function.FindSelected();
            ideal = (int)selected.getIdealTemperature();
        }

        private void OnDisable()
        {
            webSocket.Close();
        }

        public void sswitch()
        {
            this.temperatures = InitTemperatures();
            this.points = InitPoints();
            Rerender();
        }

        private float[,] InitTemperatures()
        {
            float[,] temperature = new float[vertical, horizontal];
            for (int j = 0; j < vertical; j++)
                for (int i = 0; i < horizontal; i++)
                    temperature[j, i] = bottom;
            return temperature;
        }

        private float[,] InitPoints()
        {
            float[,] points = new float[vertical, horizontal];
            for (int j = 0; j < vertical; j++)
                for (int i = 0; i < horizontal; i++)
                    points[j, i] = bottom;
            return points;
        }

        private Mesh DrawHeatMap()
        {
            Mesh mesh = new Mesh();
            mesh.name = "HeatMap";

            Vector3[] vertices = new Vector3[horizontal * vertical];
            Color[] colors = new Color[vertices.Length];
            Vector2[] uvs = new Vector2[vertices.Length];
            int[] triangles = new int[(horizontal - 1) * (vertical - 1) * 6];

            Vector3 origin = new Vector3(-size.width / 2.0f, -size.height / 2.0f, 0);
            float perWidth = size.width / horizontal;
            float perHeight = size.height / vertical;

            // vertices
            // uvs 
            // colors
            for (int j = 0; j < vertical; j++)
            {
                for (int i = 0; i < horizontal; i++)
                {
                    Vector3 vertex = origin + new Vector3(i * perWidth, j * perHeight, 0);
                    vertices[horizontal * j + i] = vertex;
                    uvs[horizontal * j + i] = new Vector2(0, 1) + new Vector2(1 / horizontal * i, 1 / vertical * j);
                    //colors[horizontal * j + i] = CalcColor(this.temperatures[j , i]);
                }
            }

            // triangles
            int count = 0;
            for (int i = 0; i < vertical - 1; i++)
            {
                for (int j = 0; j < horizontal - 1; j++)
                {
                    triangles[count] = i * horizontal + j;
                    triangles[count + 1] = (i + 1) * horizontal + j;
                    triangles[count + 2] = i * horizontal + j + 1;
                    triangles[count + 3] = i * horizontal + j + 1;
                    triangles[count + 4] = (i + 1) * horizontal + j;
                    triangles[count + 5] = (i + 1) * horizontal + j + 1;
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

        private void AddVertexColor()
        {
            Color[] colors = new Color[meshFilter.mesh.colors.Length];
            for (int j = 0; j < vertical; j++)
            {
                for (int i = 0; i < horizontal; i++)
                    colors[horizontal * j + i] = CalcColor(this.temperatures[j, i]);
            }
            meshFilter.mesh.colors = colors;
        }

        private Color CalcColor(float temperature)
        {
            //if (temperature > 10)
            //    return Color.black;
            //int count = (int)temperature / 10;
            int count;
            if (temperature > ideal)
                count = ((int)(temperature) - ideal) / step + 5;
            else
                count = -((int)(ideal - temperature) / step) + 5 - 1;
            if (count < 0) count = 0;
            if (count > 10) count = 10;
            Color[] colors = GetColors(count);
            if (colors[0] == colors[1])
                return colors[0];
            Color from = colors[0];
            Color to = colors[1];
            //float temp = (temperature % 10) / 10;
            float temp;
            if (temperature > ideal)
                temp = ((temperature - ideal) % step) / step;
            else
                temp = 1 - ((ideal - temperature) % step) / step;
            if (temp > 0.9) return to;
            if (temp < 0.1) return from;
            Color offset = to - from;
            return from + offset * temp;
        }

        // set color by your need
        private Color[] GetColors(int index)
        {
            Color startColor = Color.blue, endColor = Color.blue;
            switch (index)
            {
                //when ideal = 20:

                //step == 5: -5~0 color
                //step == 10: -30~-20 color
                case 0:
                    startColor = Color.blue;
                    endColor = Color.blue;
                    break;
                //step == 5: 0~5 color
                //step == 10: -20~-10 color
                case 1:
                    startColor = Color.blue;
                    endColor = Color.blue;
                    break;
                //step == 5: 5~10 color
                //step == 10: -10~0 color
                case 2:
                    startColor = Color.blue;
                    endColor = Color.cyan;
                    break;
                //step == 5: 10~15 color
                //step == 10: 0~10 color
                case 3:
                    startColor = Color.cyan;
                    endColor = Color.green;
                    break;
                //step == 5: 15~20 color
                //step == 10: 10~20 color
                case 4:
                    startColor = Color.green;
                    endColor = Color.green;
                    break;
                //step == 5: 20~25 color
                //step == 10: 20~30 color
                case 5:
                    startColor = Color.green;
                    endColor = Color.green;
                    break;
                //step == 5: 25~30 color
                //step == 10: 30~40 color
                case 6:
                    startColor = Color.green;
                    endColor = Color.yellow;
                    break;
                //step == 5: 30~35 color
                //step == 10: 40~50 color
                case 7:
                    startColor = Color.yellow;
                    endColor = Color.red;
                    break;
                //step == 5: 35~40 color
                //step == 10: 50~60 color
                case 8:
                    startColor = Color.red;
                    endColor = Color.magenta;
                    break;
                //step == 5: 40~45 color
                //step == 10: 60~70 color
                case 9:
                    startColor = Color.magenta;
                    endColor = Color.magenta;
                    break;
                //step == 5: 45~50 color
                //step == 10: over 70 color
                case 10:
                    startColor = Color.magenta;
                    endColor = Color.magenta;
                    break;
            }
            return new Color[] { startColor, endColor };
        }

        public void Inject()
        {
            this.temperatures = InitTemperatures();
            //this.horizontal = temperature.GetLength(1);
            //this.vertical = temperature.GetLength(0);
            injectOne(88, 39, 99);
            //RandomTeamperature(99, 50, 0, 20, ref this.temperatures);
            //RandomTeamperature(89, 50, 0, 25, ref this.temperatures);
            //RandomTeamperature(79, 50, 0, 30, ref this.temperatures);
            //RandomTeamperature(69, 50, 0, 33, ref this.temperatures);
            //RandomTeamperature(89, 50, 0, 50, ref this.temperatures);
            //RandomTeamperature(79, 50, 0, 22, ref this.temperatures);
            meshFilter.mesh = DrawHeatMap();
            AddVertexColor();
        }

        public void test()
        {
            m_garden selected = function.FindSelected();
            HTTPRequest request_getSensorData1 = new HTTPRequest(new Uri(data.IP + "/getLatestTemperatureByGardenId?gardenId=" + selected.getId()), HTTPMethods.Get, (req_data1, res_data1) =>
            {
                Debug.Log(res_data1.DataAsText);
                JArray array = JArray.Parse(res_data1.DataAsText);
                foreach (var e in array)
                    foreach (sensor n in selected.getSensors())
                        if (n.getId() == (long)e["id"] && n.getType())
                        {
                            n.setData((float)e["temperature"]);
                            updateOnePoint(n.getX(), n.getY(), n.getData());
                        }
            }).Send();

            webSocket.Open();
        }

        // Websocket
        private void init()
        {
            Debug.Log("web socket init");

            webSocket = new WebSocket(new Uri(data.wsIP + "/getAllLatestTemperature"));
            webSocket.OnOpen += OnOpen;
            webSocket.OnMessage += OnMessageReceived;
            webSocket.OnError += OnError;
            webSocket.OnClosed += OnClosed;
        }

        private void antiInit()
        {
            webSocket.OnOpen = null;
            webSocket.OnMessage = null;
            webSocket.OnError = null;
            webSocket.OnClosed = null;
            webSocket = null;
        }
        void OnOpen(WebSocket ws)
        {
            Debug.Log("connected");
            webSocket.Send(function.FindSelected().getId().ToString());
        }

        void OnMessageReceived(WebSocket ws, string message)
        {
            Debug.Log("receive: " + message);
            JObject data = (JObject)JsonConvert.DeserializeObject(message);

            long id = long.Parse((string)data["id"]);
            float temperature = float.Parse((string)data["temperature"]);

            foreach (sensor e in selected.getSensors())
                if (e.getId() == id && e.getType())
                {
                    e.setData(temperature);
                    updateOnePoint(e.getX(), e.getY(), temperature);
                }
        }

        void OnClosed(WebSocket ws, UInt16 code, string message)
        {
            Debug.Log("closing...");
            Debug.Log(message);
            antiInit();
            init();
        }

        private void OnDestroy()
        {
            if (webSocket != null && webSocket.IsOpen)
            {
                webSocket.Close();
                antiInit();
            }
        }

        /// <summary>
        /// Called when an error occured on client side
        /// </summary>
        void OnError(WebSocket ws, Exception ex)
        {
            string errorMsg = string.Empty;
#if !UNITY_WEBGL || UNITY_EDITOR
            if (ws.InternalRequest.Response != null)
                errorMsg = string.Format("Status Code from Server: {0} and Message: {1}", ws.InternalRequest.Response.StatusCode, ws.InternalRequest.Response.Message);
#endif
            antiInit();
            init();
        }

        public void updateOnePoint(int x, int y, float temperature)
        {
            this.temperatures = InitTemperatures();
            editOnePoint(x, y, temperature);
            Rerender();
        }

        public void Rerender()
        {
            for (int j = 0; j < (int)vertical; j++)
            {
                for (int i = 0; i < (int)horizontal; i++)
                {
                    if (points[j, i] != 0)
                    {
                        injectOne(i, j, points[j, i]);
                    }
                }
            }
            meshFilter.mesh = DrawHeatMap();
            AddVertexColor();
        }

        private void editOnePoint(int x, int y, float temperature)
        {
            x = (int)x / positionFX;
            y = (int)y / positionFX;
            if (x >= horizontal || x < 0 || y >= vertical || y < 0)
            {
                return;
            }
            points[y, x] = temperature;
        }

        private void injectOne(int x, int y, float temperature)
        {
            RandomTeamperature(x, y, temperature, bottom, 0, radius, ref this.temperatures);
        }

        private void RandomTeamperature(int x, int y, float from, float to, float minD, float maxD, ref float[,] temperatures)
        {
            float offset = to - from;
            maxD = (int)-offset * radius;
            float maxTweenDis = maxD - minD;
            for (int i = x - (int)maxD; i < x + maxD; i++)
            {
                for (int j = y + (int)maxD; j > y - maxD; j--)
                {
                    if (i < 0 || i >= horizontal)
                        continue;
                    if (j < 0 || j >= vertical)
                        continue;
                    float distance = Mathf.Sqrt(Mathf.Pow(x - i, 2) + Mathf.Pow(y - j, 2));
                    if (distance <= maxD && distance >= minD)
                    {
                        float offsetDis = distance - minD;
                        float ratio = offsetDis / maxTweenDis;
                        float temp = from + ratio * offset;
                        if (temp > temperatures[j, i])
                            temperatures[j, i] = temp;
                    }
                }
            }
        }

        private void TemperatureToString()
        {
            for (int y = 0; y < vertical; y++)
            {
                string input = "";
                for (int x = 0; x < horizontal; x++)
                {
                    input += this.temperatures[y, x].ToString("F2") + ",";
                }
                Debug.Log(input);
            }
        }
    }
}